namespace Orc.FeatureToggles.ViewModels;

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

    public ManageFeatureTogglesViewModel(IFeatureToggleService featureToggleService,
        ILanguageService languageService)
    {
        ArgumentNullException.ThrowIfNull(featureToggleService);
        ArgumentNullException.ThrowIfNull(languageService);

        _featureToggleService = featureToggleService;
        _languageService = languageService;

        Toggles = new FastObservableCollection<FeatureToggle>();
        ToggleFilter = string.Empty;

        Reset = new Command(OnResetExecute, OnResetCanExecute);
        Toggle = new Command(OnToggleExecute, OnToggleCanExecute);
    }

    public override string Title
    {
        get { return _languageService.GetRequiredString("FeatureToggles_ManageFeatureToggles"); }
    }

    public string ToggleFilter { get; set; }

    public FastObservableCollection<FeatureToggle> Toggles { get; }

    public FeatureToggle? SelectedToggle { get; set; }

    public Command Reset { get; }

    private bool OnResetCanExecute()
    {
        return SelectedToggle is not null;
    }

    private void OnResetExecute()
    {
        SelectedToggle?.Reset();
    }

    public Command Toggle { get; }

    private bool OnToggleCanExecute()
    {
        return SelectedToggle is not null;
    }

    private void OnToggleExecute()
    {
        SelectedToggle?.Toggle();
    }
        
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
        if (selectedToggle is not null && Toggles.Any(x => string.Equals(x.Name, selectedToggle.Name)))
        {
            SelectedToggle = selectedToggle;
        }
    }

    private void OnToggleFilterChanged()
    {
        UpdateToggles();
    }
}
