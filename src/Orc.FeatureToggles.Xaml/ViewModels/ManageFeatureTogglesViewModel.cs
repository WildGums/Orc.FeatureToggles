namespace Orc.FeatureToggles.ViewModels
{
    using Catel;
    using Catel.Collections;
    using Catel.MVVM;
    using Catel.Services;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ManageFeatureTogglesViewModel : ViewModelBase
    {
        private readonly IFeatureToggleService _featureToggleService;
        private readonly ILanguageService _languageService;
        private readonly IMessageService _messageService;

        public ManageFeatureTogglesViewModel(IFeatureToggleService featureToggleService, ICommandManager commandManager,
            ILanguageService languageService, IMessageService messageService)
        {
            Argument.IsNotNull(() => featureToggleService);
            Argument.IsNotNull(() => commandManager);
            Argument.IsNotNull(() => languageService);
            Argument.IsNotNull(() => messageService);

            _featureToggleService = featureToggleService;
            _languageService = languageService;
            _messageService = messageService;

            Toggles = new FastObservableCollection<FeatureToggle>();
            ToggleFilter = string.Empty;

            Reset = new Command(OnResetExecute, OnResetCanExecute);
            Toggle = new Command(OnToggleExecute, OnToggleCanExecute);
        }

        #region Properties
        public override string Title
        {
            get { return _languageService.GetString("FeatureToggles_ManageFeatureToggles"); }
        }

        public string ToggleFilter { get; set; }

        public FastObservableCollection<FeatureToggle> Toggles { get; private set; }

        public FeatureToggle SelectedToggle { get; set; }
        #endregion

        #region Commands
        public Command Reset { get; private set; }

        private bool OnResetCanExecute()
        {
            return SelectedToggle != null;
        }

        private void OnResetExecute()
        {
            SelectedToggle.Reset();
        }

        public Command Toggle { get; private set; }

        private bool OnToggleCanExecute()
        {
            return SelectedToggle != null;
        }

        private void OnToggleExecute()
        {
            SelectedToggle.Toggle();
        }
        #endregion

        #region Methods
        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();
        }

        protected override async Task CloseAsync()
        {
            await _featureToggleService.SaveAsync();

            await base.CloseAsync();
        }


        private void UpdateToggles()
        {
            var selectedToggle = SelectedToggle;
            var toggleFilter = ToggleFilter;

            var allToggles = _featureToggleService.GetToggles().OrderBy(x => x).ToList();
            if (!string.IsNullOrWhiteSpace(toggleFilter))
            {
                allToggles = allToggles.Where(x => x.Name.IndexOf(toggleFilter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }

            using (Toggles.SuspendChangeNotifications())
            {
                Toggles.Clear();

                foreach (var toggle in allToggles)
                {
                    if (!toggle.IsHidden)
                    {
                        Toggles.Add(toggle);
                    }
                }
            }

            // restore selection
            if (selectedToggle != null && Toggles.Any(x => string.Equals(x.Name, selectedToggle)))
            {
                SelectedToggle = selectedToggle;
            }
        }

        private void OnToggleFilterChanged()
        {
            UpdateToggles();
        }
        #endregion
    }
}
