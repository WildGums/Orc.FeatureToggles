namespace Orc.FeatureToggles;

using System;
using Catel.Data;

public class FeatureToggle : ObservableObject, IComparable<FeatureToggle>, IComparable
{
    private bool? _value;

    public FeatureToggle()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

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

    public bool EffectiveValue
    {
        get { return Value ?? DefaultValue; }
    }

    public bool DefaultValue { get; set; }

    public event EventHandler<ToggledEventArgs>? Toggled;

    protected void RaiseToggled(bool? oldValue, bool? newValue)
    {
        Toggled?.Invoke(this, new ToggledEventArgs(this, oldValue, newValue));
    }

    public int CompareTo(FeatureToggle? other)
    {
        if (other is null)
        {
            return -1;
        }

        return Name.CompareTo(other.Name);
    }

    public int CompareTo(object? obj)
    {
        if (obj is FeatureToggle toggle)
        {
            return CompareTo(toggle);
        }

        return -1;
    }

    public override string ToString()
    {
        return $"{Name} ({Value})";
    }
}
