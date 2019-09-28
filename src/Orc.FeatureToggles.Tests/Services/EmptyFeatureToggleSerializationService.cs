namespace Orc.FeatureToggles.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EmptyFeatureToggleSerializationService : IFeatureToggleSerializationService
    {
        public async Task<List<FeatureToggle>> LoadAsync()
        {
            return new List<FeatureToggle>();
        }

        public async Task SaveAsync(List<FeatureToggle> featureToggles)
        {
            
        }
    }
}
