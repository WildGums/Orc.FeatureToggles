namespace Orc.FeatureToggles
{
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
