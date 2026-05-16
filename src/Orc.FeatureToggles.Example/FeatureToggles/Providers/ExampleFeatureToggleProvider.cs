namespace Orc.FeatureToggles.Example.FeatureToggles.Providers;

using System.Collections.Generic;
using System.Threading.Tasks;
using Catel.Services;

public class ExampleFeatureToggleProvider : IFeatureToggleProvider
{
    private readonly ILanguageService _languageService;

    public ExampleFeatureToggleProvider(ILanguageService languageService)
    {
        _languageService = languageService;
    }

    public Task<IReadOnlyList<FeatureToggle>> ProvideTogglesAsync()
    {
        var superCoolFeatureToggle = new SuperCoolFeatureToggle
        {
            Description = _languageService.GetRequiredString("Orc_FeatureToggles_Example_SuperCoolFeatureToggle_Description")
        };

        var toggles = new List<FeatureToggle>
        {
            superCoolFeatureToggle
        };

        return Task.FromResult<IReadOnlyList<FeatureToggle>>(toggles);
    }
}
