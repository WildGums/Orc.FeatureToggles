namespace Orc.FeatureToggles.Example.FeatureToggles.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class ExampleFeatureToggleProvider : IFeatureToggleProvider
    {
        public async Task<List<FeatureToggle>> ProvideTogglesAsync()
        {
            var toggles = new List<FeatureToggle>();

            toggles.Add(new SuperCoolFeatureToggle());

            return toggles;
        }
    }
}
