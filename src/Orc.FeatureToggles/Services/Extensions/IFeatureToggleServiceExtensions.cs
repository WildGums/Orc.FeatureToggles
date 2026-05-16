namespace Orc.FeatureToggles;

using System;
using System.Globalization;
using System.Threading.Tasks;
using Catel.IoC;
using Catel.Services;
using Microsoft.Extensions.DependencyInjection;

public static class IFeatureToggleServiceExtensions
{
    public static FeatureToggle GetRequiredToggle(this IFeatureToggleService service, string name)
    {
        ArgumentNullException.ThrowIfNull(service);

        var toggle = service.GetToggle(name);
        if (toggle is null)
        {
            var languageService = IoCContainer.ServiceProvider?.GetService<ILanguageService>();
            var message = languageService is not null
                ? languageService.GetRequiredStringAndFormat("Orc_FeatureToggles_IFeatureToggleServiceExtensions_RequiredToggleNotFound", name)
                : string.Format(CultureInfo.CurrentCulture, "Could not find required toggle '{0}'", name);

            throw new InvalidOperationException(message);
        }

        return toggle;
    }

    public static bool GetValue(this IFeatureToggleService service, string name, bool fallbackValue)
    {
        ArgumentNullException.ThrowIfNull(service);

        var toggle = service.GetToggle(name);
        return toggle?.EffectiveValue ?? fallbackValue;
    }

    public static bool RemoveToggle(this IFeatureToggleService service, string name)
    {
        ArgumentNullException.ThrowIfNull(service);

        var toggle = service.GetToggle(name);
        return toggle is not null && service.RemoveToggle(toggle);
    }

    public static bool Toggle(this IFeatureToggleService service, string name)
    {
        ArgumentNullException.ThrowIfNull(service);

        var toggle = service.GetToggle(name);
        if (toggle is null)
        {
            return false;
        }

        toggle.Toggle();
        return true;
    }

    public static async Task InitializeAndLoadAsync(this IFeatureToggleService service)
    {
        ArgumentNullException.ThrowIfNull(service);

        await service.InitializeAsync();
        await service.LoadAsync();
    }
}
