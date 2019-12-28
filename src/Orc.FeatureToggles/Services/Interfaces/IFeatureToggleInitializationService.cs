namespace Orc.FeatureToggles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IFeatureToggleInitializationService
    {
        Task<List<FeatureToggle>> FindTogglesAsync();
    }
}
