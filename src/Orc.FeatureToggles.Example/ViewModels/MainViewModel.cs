namespace Orc.FeatureToggles.Example.ViewModels;

using System;
using Catel.MVVM;

public class MainViewModel : ViewModelBase
{
    private readonly IFeatureToggleService _featureToggleService;
    
    public MainViewModel(IServiceProvider serviceProvider, IFeatureToggleService featureToggleService)
        : base(serviceProvider)
    {
        _featureToggleService = featureToggleService;
    }

    public override string Title => "Feature Toggles Test";
}
