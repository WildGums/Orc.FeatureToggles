namespace Orc.FeatureToggles.Tests.Services;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class EmptyFeatureToggleSerializationService : IFeatureToggleSerializationService
{
    public async Task<FeatureToggleValue[]> LoadAsync()
    {
        return Array.Empty<FeatureToggleValue>();
    }

    public async Task SaveAsync(IEnumerable<FeatureToggleValue> toggleValues)
    {
            
    }
}
