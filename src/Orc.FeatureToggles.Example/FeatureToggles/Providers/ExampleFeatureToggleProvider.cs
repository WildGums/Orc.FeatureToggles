namespace Orc.FeatureToggles.Example.FeatureToggles.Providers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ExampleFeatureToggleProvider : IFeatureToggleProvider
    {
        public async Task<IEnumerable<FeatureToggle>> ProvideTogglesAsync()
        {
            var toggles = new List<FeatureToggle>();

            toggles.Add(new SuperCoolFeatureToggle());

            return toggles;
        }
    }
}
