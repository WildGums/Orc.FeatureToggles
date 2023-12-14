namespace Orc.FeatureToggles.Example.FeatureToggles.Providers;

using System.Collections.Generic;
using System.Threading.Tasks;

public class ExampleFeatureToggleProvider : IFeatureToggleProvider
{
    public async Task<IReadOnlyList<FeatureToggle>> ProvideTogglesAsync()
    {
        var toggles = new List<FeatureToggle>
        {
            new SuperCoolFeatureToggle()
        };

        return toggles;
    }
}
