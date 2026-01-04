namespace Orc.FeatureToggles
{
    using Catel.Services;
    using Catel.ThirdPartyNotices;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    /// <summary>
    /// Core module which allows the registration of default services in the service collection.
    /// </summary>
    public static class OrcFeatureTogglesModule
    {
        public static IServiceCollection AddOrcFeatureToggles(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IFeatureToggleService, FeatureToggleService>();
            serviceCollection.TryAddSingleton<IFeatureToggleInitializationService, FeatureToggleInitializationService>();
            serviceCollection.TryAddSingleton<IFeatureToggleSerializationService, FeatureToggleSerializationService>();

            serviceCollection.AddSingleton<ILanguageSource>(new LanguageResourceSource("Orc.FeatureToggles", "Orc.FeatureToggles.Properties", "Resources"));

            serviceCollection.AddSingleton<IThirdPartyNotice>((x) => new LibraryThirdPartyNotice("Orc.FeatureToggles", "https://github.com/wildgums/orc.featuretoggles"));

            return serviceCollection;
        }
    }
}
