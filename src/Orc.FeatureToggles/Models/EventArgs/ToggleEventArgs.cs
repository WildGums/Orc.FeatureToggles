namespace Orc.FeatureToggles
{
    using System;
    using Catel;

    public class ToggleEventArgs : EventArgs
    {
        public ToggleEventArgs(FeatureToggle toggle)
        {
            ArgumentNullException.ThrowIfNull(toggle);

            Toggle = toggle;
        }

        public FeatureToggle Toggle { get; }

        public bool IsToggle(string name)
        {
            return Toggle.Name.EqualsIgnoreCase(name);
        }
    }
}
