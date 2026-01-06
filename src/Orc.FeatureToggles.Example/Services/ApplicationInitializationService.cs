namespace Orc.FeatureToggles.Example.Services;

using System;
using System.Threading.Tasks;
using System.Windows.Media;
using Orchestra;

public class ApplicationInitializationService : ApplicationInitializationServiceBase
{
    private readonly IFeatureToggleService _featureToggleService;

    public ApplicationInitializationService(IServiceProvider serviceProvider, 
        IFeatureToggleService featureToggleService)
        : base(serviceProvider)
    {
        _featureToggleService = featureToggleService;
    }

    public override async Task InitializeBeforeCreatingShellAsync()
    {
        InitializeFonts();

        await InitializeFeatureTogglesAsync();
    }

    private void InitializeFonts()
    {
        Orc.Theming.FontImage.DefaultBrush = new SolidColorBrush(Color.FromArgb(255, 87, 87, 87));
    }

    private async Task InitializeFeatureTogglesAsync()
    {
        await _featureToggleService.InitializeAndLoadAsync();
    }
}
