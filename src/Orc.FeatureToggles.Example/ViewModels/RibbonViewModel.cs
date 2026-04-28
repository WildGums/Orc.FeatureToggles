namespace Orc.FeatureToggles.Example.ViewModels;

using System;
using System.Threading.Tasks;
using Catel.Logging;
using Catel.MVVM;
using Catel.Services;
using Orc.FeatureToggles.ViewModels;

public class RibbonViewModel : ViewModelBase
{
    private readonly IUIVisualizerService _uiVisualizerService;
    private readonly IFeatureToggleService _featureToggleService;

    public RibbonViewModel(IServiceProvider serviceProvider, IFeatureToggleService featureToggleService, 
        IUIVisualizerService uiVisualizerService)
        : base(serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(featureToggleService);
        ArgumentNullException.ThrowIfNull(uiVisualizerService);

        _featureToggleService = featureToggleService;
        _uiVisualizerService = uiVisualizerService;

        ManageFeatureToggles = new TaskCommand(serviceProvider, OnManageFeatureTogglesExecuteAsync);
    }

    public TaskCommand ManageFeatureToggles { get; private set; }

    private async Task OnManageFeatureTogglesExecuteAsync()
    {
        await _uiVisualizerService.ShowDialogAsync<ManageFeatureTogglesViewModel>();
    }
}
