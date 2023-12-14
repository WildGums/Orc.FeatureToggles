namespace Orc.FeatureToggles.Example.Services;

using System;
using System.Threading.Tasks;
using System.Windows.Media;
using Catel.IoC;
using Orchestra.Services;

public class ApplicationInitializationService : ApplicationInitializationServiceBase
{
    private readonly IServiceLocator _serviceLocator;

    public ApplicationInitializationService(IServiceLocator serviceLocator)
    {
        ArgumentNullException.ThrowIfNull(serviceLocator);

        _serviceLocator = serviceLocator;
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
        var featureToggleService = _serviceLocator.ResolveType<IFeatureToggleService>();

        await featureToggleService.InitializeAndLoadAsync();
    }
}
