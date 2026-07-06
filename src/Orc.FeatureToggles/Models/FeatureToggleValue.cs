namespace Orc.FeatureToggles;

using System;

public class FeatureToggleValue
{
    public FeatureToggleValue()
    {
        Name = string.Empty;
    }

    public FeatureToggleValue(FeatureToggle toggle)
    {
        ArgumentNullException.ThrowIfNull(toggle);

        Name = toggle.Name;
        Value = toggle.Value;
    }

    public string Name { get; set; }

    public bool? Value { get; set; }
}
