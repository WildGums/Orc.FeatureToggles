namespace Orc.FeatureToggles;

using System;

public static class FeatureToggleExtensions
{
    public static void Reset(this FeatureToggle toggle)
    {
        ArgumentNullException.ThrowIfNull(toggle);

        toggle.Value = null;
    }

    public static void Toggle(this FeatureToggle toggle)
    {
        ArgumentNullException.ThrowIfNull(toggle);

        if (!toggle.Value.HasValue)
        {
            // Was using default value, so switch to non-default
            toggle.Value = !toggle.DefaultValue;
        }
        else
        {
            toggle.Value = !toggle.Value;
        }
    }
}
