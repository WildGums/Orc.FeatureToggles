namespace Orc.FeatureToggles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Data;

    public class FeatureToggleValue : ModelBase
    {
        public FeatureToggleValue()
        {

        }

        public FeatureToggleValue(FeatureToggle toggle)
        {
            Argument.IsNotNull(() => toggle);

            Name = toggle.Name;
            Value = toggle.Value;
        }

        public string Name { get; set; }

        public bool? Value { get; set; }
    }
}
