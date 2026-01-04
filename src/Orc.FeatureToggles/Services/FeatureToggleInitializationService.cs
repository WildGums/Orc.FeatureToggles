namespace Orc.FeatureToggles;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MethodTimer;
using Microsoft.Extensions.Logging;

public class FeatureToggleInitializationService : IFeatureToggleInitializationService
{
    private readonly ILogger<FeatureToggleInitializationService> _logger;
    private readonly IReadOnlyList<IFeatureToggleProvider> _featureToggleProviders;

    public FeatureToggleInitializationService(ILogger<FeatureToggleInitializationService> logger,
        IEnumerable<IFeatureToggleProvider> featureToggleProviders)
    {
        _logger = logger;
        _featureToggleProviders = featureToggleProviders.ToArray();
    }

    [Time]
    public async Task<IReadOnlyList<FeatureToggle>> FindTogglesAsync()
    {
        var toggles = new List<FeatureToggle>();

        _logger.LogDebug("Searching feature toggles");

        foreach (var featureToggleProvider in _featureToggleProviders)
        {
            try
            {
                var providerToggles = await featureToggleProvider.ProvideTogglesAsync();

                toggles.AddRange(providerToggles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to retrieve feature toggles from provider '{featureToggleProvider.GetType().Name}'");
            }
        }

        _logger.LogDebug($"Found '{toggles.Count}' feature toggles");

        return toggles.ToArray();
    }
}
