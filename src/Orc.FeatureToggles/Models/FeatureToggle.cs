namespace Orc.FeatureToggles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Catel.Data;

    public class FeatureToggle : ObservableObject
    {
        private bool? _value;

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsHidden { get; set; }

        public bool? Value
        {
            get => _value;
            set
            {
                // Note: this way we keep the value null in case it matches the default
                var currentValue = Value;
                if (currentValue != value)
                {
                    _value = value;
                    RaiseToggled(currentValue, value);
                    RaisePropertyChanged(nameof(Value));
                }
            }
        }

        public bool DefaultValue { get; set; }

        public event EventHandler<ToggledEventArgs> Toggled;

        protected void RaiseToggled(bool? oldValue, bool? newValue)
        {
            Toggled?.Invoke(this, new ToggledEventArgs(this, oldValue, newValue));
        }

        public override string ToString()
        {
            return $"{Name} ({Value})";
        }
    }
}
