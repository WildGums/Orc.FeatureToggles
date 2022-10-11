using Catel.IoC;
using Catel.Services;
using Orc.FeatureToggles;

/// <summary>
/// Used by the ModuleInit. All code inside the Initialize method is ran as soon as the assembly is loaded.
/// </summary>
public static class ModuleInitializer
{
    /// <summary>
    /// Initializes the module.
    /// </summary>
    public static void Initialize()
    {
        var serviceLocator = ServiceLocator.Default;

        serviceLocator.RegisterTypeIfNotYetRegistered<IFeatureToggleService, FeatureToggleService>();
        serviceLocator.RegisterTypeIfNotYetRegistered<IFeatureToggleInitializationService, FeatureToggleInitializationService>();
        serviceLocator.RegisterTypeIfNotYetRegistered<IFeatureToggleSerializationService, FeatureToggleSerializationService>();

        var languageService = serviceLocator.ResolveRequiredType<ILanguageService>();
        languageService.RegisterLanguageSource(new LanguageResourceSource("Orc.FeatureToggles", "Orc.FeatureToggles.Properties", "Resources"));
    }
}
