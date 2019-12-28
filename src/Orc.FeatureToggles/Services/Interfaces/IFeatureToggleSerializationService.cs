namespace Orc.FeatureToggles
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    public interface IFeatureToggleSerializationService
    {
        Task<List<FeatureToggleValue>> LoadAsync();
        Task SaveAsync(List<FeatureToggleValue> toggleValues);
    }
}
