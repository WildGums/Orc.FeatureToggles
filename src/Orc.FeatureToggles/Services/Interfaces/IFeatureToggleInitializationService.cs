namespace Orc.FeatureToggles;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFeatureToggleInitializationService
{
    Task<IReadOnlyList<FeatureToggle>> FindTogglesAsync();
}
