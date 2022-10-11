namespace Orc.FeatureToggles.Example.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Collections;
    using Catel.Logging;
    using Catel.MVVM;
    using Catel.Reflection;
    using Catel.Services;
    using Orc.FeatureToggles.ViewModels;

    public class RibbonViewModel : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IUIVisualizerService _uiVisualizerService;
        private readonly IFeatureToggleService _featureToggleService;

        public RibbonViewModel(IFeatureToggleService featureToggleservice, IUIVisualizerService uiVisualizerService)
        {
            ArgumentNullException.ThrowIfNull(featureToggleservice);
            ArgumentNullException.ThrowIfNull(uiVisualizerService);

            _featureToggleService = featureToggleservice;
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
}
