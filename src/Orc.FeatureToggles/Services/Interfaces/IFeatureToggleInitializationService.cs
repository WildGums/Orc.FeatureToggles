namespace Orc.FeatureToggles;

using System.Threading.Tasks;

public interface IFeatureToggleInitializationService
{
    Task<FeatureToggle[]> FindTogglesAsync();
}
