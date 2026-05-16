namespace Orc.FeatureToggles.Example.ViewModels;

using System;
using System.Threading.Tasks;
using Catel.MVVM;
using Catel.Services;
using FeatureToggles;

public class StatusBarViewModel : ViewModelBase
{
    private readonly IFeatureToggleService _featureToggleService;
    private readonly ILanguageService _languageService;

    public StatusBarViewModel(IServiceProvider serviceProvider, IFeatureToggleService featureToggleService,
        ILanguageService languageService)
        : base(serviceProvider)
    {
        _featureToggleService = featureToggleService;
        _languageService = languageService;
    }

    public string Status { get; private set; }

    protected override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        _featureToggleService.Toggled += OnFeatureToggleServiceToggled;

        Update();
    }

    protected override async Task CloseAsync()
    {
        _featureToggleService.Toggled -= OnFeatureToggleServiceToggled;

        await base.CloseAsync();
    }

    private void OnFeatureToggleServiceToggled(object sender, ToggledEventArgs e)
    {
        if (e.IsToggle(SuperCoolFeatureToggle.Name))
        {
            Update();
        }
    }

    private void Update()
    {
        var text = _languageService.GetRequiredString("Orc_FeatureToggles_Example_StatusBarViewModel_SuperCoolFeatureDisabled");

        if (_featureToggleService.GetValue(SuperCoolFeatureToggle.Name, false))
        {
            text = _languageService.GetRequiredString("Orc_FeatureToggles_Example_StatusBarViewModel_SuperCoolFeatureEnabled");
        }

        Status = text;
    }
}
