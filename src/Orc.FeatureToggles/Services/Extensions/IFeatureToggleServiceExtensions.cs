namespace Orc.FeatureToggles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Catel;

    public static class IFeatureToggleServiceExtensions
    {
        public static bool GetValue(this IFeatureToggleService service, string name, bool fallbackValue)
        {
            Argument.IsNotNull(() => service);

            var toggle = service.GetToggle(name);
            if (toggle is null)
            {
                return fallbackValue;
            }

            return toggle.Value ?? toggle.DefaultValue;
        }

        public static bool RemoveToggle(this IFeatureToggleService service, string name)
        {
            Argument.IsNotNull(() => service);

            var toggle = service.GetToggle(name);
            if (toggle is null)
            {
                return false;
            }

            return service.RemoveToggle(toggle);
        }

        public static bool Toggle(this IFeatureToggleService service, string name)
        {
            Argument.IsNotNull(() => service);

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
            Argument.IsNotNull(() => service);

            await service.InitializeAsync();
            await service.LoadAsync();
        }
    }
}
