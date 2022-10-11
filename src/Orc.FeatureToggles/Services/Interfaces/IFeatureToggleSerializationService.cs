namespace Orc.FeatureToggles
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFeatureToggleSerializationService
    {
        Task<FeatureToggleValue[]> LoadAsync();
        Task SaveAsync(IEnumerable<FeatureToggleValue> toggleValues);
    }
}
