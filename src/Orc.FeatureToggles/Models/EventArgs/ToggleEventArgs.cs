namespace Orc.FeatureToggles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Catel;

    public class ToggleEventArgs : EventArgs
    {
        public ToggleEventArgs(FeatureToggle toggle)
        {
            Argument.IsNotNull(() => toggle);

            Toggle = toggle;
        }

        public FeatureToggle Toggle { get; }

        public bool IsToggle(string name)
        {
            return Toggle.Name.EqualsIgnoreCase(name);
        }
    }
}
