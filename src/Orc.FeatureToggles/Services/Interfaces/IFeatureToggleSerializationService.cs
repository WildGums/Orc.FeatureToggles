namespace Orc.FeatureToggles;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFeatureToggleSerializationService
{
    Task<IReadOnlyList<FeatureToggleValue>> LoadAsync();
    Task SaveAsync(IReadOnlyList<FeatureToggleValue> toggleValues);
}
