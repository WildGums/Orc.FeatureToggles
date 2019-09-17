namespace Orc.FeatureToggles.Example.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel.Collections;
    using Catel.Logging;
    using Catel.MVVM;
    using Catel.Reflection;
    using Catel.Services;

    public class RibbonViewModel : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IUIVisualizerService _uiVisualizerService;
        private readonly IFeatureToggleService _featureToggleService;

        public RibbonViewModel(IFeatureToggleService featureToggleservice, IUIVisualizerService uiVisualizerService)
        {
            _featureToggleService = featureToggleservice;
            _uiVisualizerService = uiVisualizerService;
        }

    }
}
