namespace Orc.FeatureToggles.Example.FeatureToggles;

public class SuperCoolFeatureToggle : FeatureToggle
{
    public new const string Name = "Super cool feature";

    public SuperCoolFeatureToggle()
    {
        base.Name = Name;
        Description = "When this feature toggle is enabled, it will show SUPER COOL FEATURE in the status bar";
    }
}
