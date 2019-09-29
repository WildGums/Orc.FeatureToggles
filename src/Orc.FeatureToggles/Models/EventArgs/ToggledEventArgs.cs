namespace Orc.FeatureToggles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Catel;

    public class ToggledEventArgs : ToggleEventArgs
    {
        public ToggledEventArgs(FeatureToggle toggle, bool? oldValue, bool? newValue)
            : base(toggle)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public bool? OldValue { get; }

        public bool? NewValue { get; }
    }
}
