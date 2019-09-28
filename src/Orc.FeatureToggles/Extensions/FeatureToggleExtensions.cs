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
        public static void Toggle(this FeatureToggle toggle)
        {
            Argument.IsNotNull(() => toggle);

            toggle.Value = !toggle.Value;
        }
    }
}
