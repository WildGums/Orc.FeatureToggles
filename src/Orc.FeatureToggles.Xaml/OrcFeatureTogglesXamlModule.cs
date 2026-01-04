namespace Orc.FeatureToggles
{
    using Catel.IoC;
    using Catel.Services;
    using Catel.ThirdPartyNotices;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Orc.FeatureToggles.ViewModels;
    using Orc.FeatureToggles.Views;

    /// <summary>
    /// Core module which allows the registration of default services in the service collection.
    /// </summary>
    public static class OrcFeatureTogglesXamlModule
    {
        public static IServiceCollection AddOrcFeatureTogglesXaml(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IFeatureToggleService, FeatureToggleService>();
            serviceCollection.TryAddSingleton<IFeatureToggleInitializationService, FeatureToggleInitializationService>();
            serviceCollection.TryAddSingleton<IFeatureToggleSerializationService, FeatureToggleSerializationService>();

            serviceCollection.TryAddSingleton<UIVisualizerInitializer>();

            serviceCollection.AddSingleton<ILanguageSource>(new LanguageResourceSource("Orc.FeatureToggles.Xaml", "Orc.FeatureToggles.Properties", "Resources"));

            return serviceCollection;
        }

        private class UIVisualizerInitializer : IConstructAtStartup
        {
            public UIVisualizerInitializer(IUIVisualizerService uiVisualizerService)
            {
                uiVisualizerService.Register<ManageFeatureTogglesViewModel, ManageFeatureTogglesWindow>(false);
            }
        }
    }
}
