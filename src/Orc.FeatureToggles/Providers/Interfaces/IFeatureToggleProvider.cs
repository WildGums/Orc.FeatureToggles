namespace Orc.FeatureToggles
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFeatureToggleProvider
    {
        Task<IEnumerable<FeatureToggle>> ProvideTogglesAsync(); 
    }
}
