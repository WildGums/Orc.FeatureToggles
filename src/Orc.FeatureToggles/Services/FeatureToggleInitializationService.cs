namespace Orc.FeatureToggles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Catel;
    using Catel.IoC;
    using Catel.Logging;
    using Catel.Reflection;
    using MethodTimer;

    public class FeatureToggleInitializationService : IFeatureToggleInitializationService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly ITypeFactory _typeFactory;

        public FeatureToggleInitializationService(ITypeFactory typeFactory)
        {
            Argument.IsNotNull(() => typeFactory);

            _typeFactory = typeFactory;
        }

        [Time]
        public async Task<List<FeatureToggle>> FindTogglesAsync()
        {
            var toggles = new List<FeatureToggle>();

            Log.Debug("Searching feature toggles");

            var providers = TypeCache.GetTypesImplementingInterface(typeof(IFeatureToggleProvider));

            foreach (var providerType in providers)
            {
                Log.Debug($"Using provider '{providerType.Name}' to find toggles");

                try
                {
                    var provider = (IFeatureToggleProvider)_typeFactory.CreateInstance(providerType);
                    var providerToggles = await provider.ProvideTogglesAsync();

                    Log.Debug($"Found '{providerToggles.Count}' feature toggles using provider '{providerType.Name}'");

                    toggles.AddRange(providerToggles);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"Failed to retrieve feature toggles from provider '{providerType.Name}'");
                }
            }

            Log.Debug($"Found '{toggles.Count}' feature toggles");

            return toggles;
        }
    }
}
