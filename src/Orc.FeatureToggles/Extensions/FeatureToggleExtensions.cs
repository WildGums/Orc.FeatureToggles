namespace Orc.FeatureToggles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Catel;

    public static class FeatureToggleExtensions
    {
        public static void Reset(this FeatureToggle toggle)
        {
            Argument.IsNotNull(() => toggle);

            toggle.Value = null;
        }

        public static void Toggle(this FeatureToggle toggle)
        {
            Argument.IsNotNull(() => toggle);

            if (!toggle.Value.HasValue)
            {
                // Was using default value, so siwtch to non-default
                toggle.Value = !toggle.DefaultValue;
            }
            else
            {
                toggle.Value = !toggle.Value;
            }
        }
    }
}
