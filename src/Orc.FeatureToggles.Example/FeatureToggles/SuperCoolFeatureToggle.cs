namespace Orc.FeatureToggles.Example.FeatureToggles
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SuperCoolFeatureToggle : FeatureToggle
    {
        public SuperCoolFeatureToggle()
        {
            Name = "Super cool feature";
            Description = "When this feature toggle is enabled, it will show SUPER COOL FEATURE in the status bar";
        }
    }
}
