namespace Orc.FeatureToggles.Example.ViewModels;

using System;
using System.Threading.Tasks;
using Catel.MVVM;
using FeatureToggles;

public class StatusBarViewModel : ViewModelBase
{
    private readonly IFeatureToggleService _featureToggleService;

    public StatusBarViewModel(IServiceProvider serviceProvider, IFeatureToggleService featureToggleService)
        : base(serviceProvider)
    {
        _featureToggleService = featureToggleService;
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
        var text = "Super cool feature NOT enabled";

        if (_featureToggleService.GetValue(SuperCoolFeatureToggle.Name, false))
        {
            text = "Super cool feature ENABLED";
        }

        Status = text;
    }
}
