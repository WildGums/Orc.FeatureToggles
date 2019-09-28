namespace Orc.FeatureToggles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Catel;

    public class ToggledEventArgs : EventArgs
    {
        public ToggledEventArgs(FeatureToggle toggle, bool oldValue, bool newValue)
        {
            Argument.IsNotNull(() => toggle);

            Toggle = toggle;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public FeatureToggle Toggle { get; }

        public bool OldValue { get; }

        public bool NewValue { get; }
    }
}
