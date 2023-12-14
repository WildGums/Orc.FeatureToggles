namespace Orc.FeatureToggles.Example.ViewModels;

using System;
using Catel.Logging;
using Catel.MVVM;

public class MainViewModel : ViewModelBase
{
    private static readonly ILog Log = LogManager.GetCurrentClassLogger();

    private readonly IFeatureToggleService _featureToggleService;
    
    public MainViewModel(IFeatureToggleService featureToggleService)
    {
        ArgumentNullException.ThrowIfNull(featureToggleService);

        _featureToggleService = featureToggleService;
    }

    public override string Title => "Feature Toggles Test";
}
