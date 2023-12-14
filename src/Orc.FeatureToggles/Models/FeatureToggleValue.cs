namespace Orc.FeatureToggles;

using System;
using Catel.Data;

public class FeatureToggleValue : ModelBase
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
