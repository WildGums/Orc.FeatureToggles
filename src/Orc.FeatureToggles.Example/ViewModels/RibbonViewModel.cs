namespace Orc.FeatureToggles.Example.ViewModels;

using System;
using System.Threading.Tasks;
using Catel.Logging;
using Catel.MVVM;
using Catel.Services;
using Orc.FeatureToggles.ViewModels;

public class RibbonViewModel : ViewModelBase
{
    private static readonly ILog Log = LogManager.GetCurrentClassLogger();

    private readonly IUIVisualizerService _uiVisualizerService;
    private readonly IFeatureToggleService _featureToggleService;

    public RibbonViewModel(IFeatureToggleService featureToggleService, IUIVisualizerService uiVisualizerService)
    {
        ArgumentNullException.ThrowIfNull(featureToggleService);
        ArgumentNullException.ThrowIfNull(uiVisualizerService);

        _featureToggleService = featureToggleService;
        _uiVisualizerService = uiVisualizerService;

        ManageFeatureToggles = new TaskCommand(OnManageFeatureTogglesExecuteAsync);
    }

    #region Commands
    public TaskCommand ManageFeatureToggles { get; private set; }

    private async Task OnManageFeatureTogglesExecuteAsync()
    {
        await _uiVisualizerService.ShowDialogAsync<ManageFeatureTogglesViewModel>();
    }
    #endregion
}
