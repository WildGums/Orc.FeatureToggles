namespace Orc.FeatureToggles
{
    using System;
    using System.Threading.Tasks;

    public static class IFeatureToggleServiceExtensions
    {
        public static FeatureToggle GetRequiredToggle(this IFeatureToggleService service, string name)
        {
            ArgumentNullException.ThrowIfNull(service);

            var toggle = service.GetToggle(name);
            if (toggle is null)
            {
                throw new InvalidOperationException($"Could not find required toggle '{name}'");
            }

            return toggle;
        }

        public static bool GetValue(this IFeatureToggleService service, string name, bool fallbackValue)
        {
            ArgumentNullException.ThrowIfNull(service);

            var toggle = service.GetToggle(name);
            if (toggle is null)
            {
                return fallbackValue;
            }

            return toggle.EffectiveValue;
        }

        public static bool RemoveToggle(this IFeatureToggleService service, string name)
        {
            ArgumentNullException.ThrowIfNull(service);

            var toggle = service.GetToggle(name);
            if (toggle is null)
            {
                return false;
            }

            return service.RemoveToggle(toggle);
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
}
