﻿using Catel.IoC;
using Catel.Services;
using Orc.FeatureToggles.ViewModels;
using Orc.FeatureToggles.Views;

/// <summary>
/// Used by the ModuleInit. All code inside the Initialize method is ran as soon as the assembly is loaded.
/// </summary>
public static class ModuleInitializer
{
    #region Methods
    /// <summary>
    /// Initializes the module.
    /// </summary>
    public static void Initialize()
    {
        var serviceLocator = ServiceLocator.Default;

        var languageService = serviceLocator.ResolveType<ILanguageService>();
        languageService.RegisterLanguageSource(new LanguageResourceSource("Orc.FeatureToggles.Xaml", "Orc.FeatureToggles.Properties", "Resources"));

        // Custom views (sharing same view model)
        var uiVisualizerService = serviceLocator.ResolveType<IUIVisualizerService>();
        uiVisualizerService.Register<ManageFeatureTogglesViewModel, ManageFeatureTogglesWindow>(false);
    }
    #endregion
}
