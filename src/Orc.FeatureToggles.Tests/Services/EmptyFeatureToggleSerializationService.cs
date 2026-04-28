namespace Orc.FeatureToggles.Tests.Services;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class EmptyFeatureToggleSerializationService : IFeatureToggleSerializationService
{
    public async Task<IReadOnlyList<FeatureToggleValue>> LoadAsync()
    {
        return Array.Empty<FeatureToggleValue>();
    }

    public async Task SaveAsync(IReadOnlyList<FeatureToggleValue> toggleValues)
    {
            
    }
}
