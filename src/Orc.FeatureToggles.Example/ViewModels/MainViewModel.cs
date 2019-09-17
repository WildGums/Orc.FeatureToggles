namespace Orc.FeatureToggles.Example.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Catel;
    using Catel.Collections;
    using Catel.Logging;
    using Catel.MVVM;

    public class MainViewModel : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IFeatureToggleService _featureToggleService;

        #region Constructors
        public MainViewModel(IFeatureToggleService featureToggleService)
        {
            Argument.IsNotNull(() => featureToggleService);

            _featureToggleService = featureToggleService;

            Title = "Orc.FeatureToggles example";
        }
        #endregion

        #region Properties
        public override string Title
        {
            get { return "Feature Toggles Test"; }
        }
        #endregion
    }
}
