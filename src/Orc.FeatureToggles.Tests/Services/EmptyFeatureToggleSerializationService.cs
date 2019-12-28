namespace Orc.FeatureToggles.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EmptyFeatureToggleSerializationService : IFeatureToggleSerializationService
    {
        public async Task<List<FeatureToggleValue>> LoadAsync()
        {
            return new List<FeatureToggleValue>();
        }

        public async Task SaveAsync(List<FeatureToggleValue> toggleValues)
        {
            
        }
    }
}
