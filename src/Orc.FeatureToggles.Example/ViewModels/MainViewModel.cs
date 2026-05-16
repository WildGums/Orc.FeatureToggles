namespace Orc.FeatureToggles.Example.ViewModels;

using System;
using Catel.MVVM;
using Catel.Services;

public class MainViewModel : ViewModelBase
{
    private readonly IFeatureToggleService _featureToggleService;
    private readonly ILanguageService _languageService;
    
    public MainViewModel(IServiceProvider serviceProvider, IFeatureToggleService featureToggleService,
        ILanguageService languageService)
        : base(serviceProvider)
    {
        _featureToggleService = featureToggleService;
        _languageService = languageService;
    }

    public override string Title => _languageService.GetRequiredString("Orc_FeatureToggles_Example_MainViewModel_Title");
}
