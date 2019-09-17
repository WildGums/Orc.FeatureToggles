[assembly: System.Resources.NeutralResourcesLanguageAttribute("en-US")]
[assembly: System.Runtime.Versioning.TargetFrameworkAttribute(".NETFramework,Version=v4.6", FrameworkDisplayName=".NET Framework 4.6")]
public class static ModuleInitializer
{
    public static void Initialize() { }
}
namespace Orc.FeatureToggles
{
    public class FeatureToggle
    {
        public FeatureToggle() { }
    }
    public class FeatureToggleSerializationService : Orc.FeatureToggles.IFeatureToggleSerializationService
    {
        public FeatureToggleSerializationService(Orc.FileSystem.IDirectoryService directoryService, Orc.FileSystem.IFileService fileService, Catel.Runtime.Serialization.Xml.IXmlSerializer xmlSerializer) { }
    }
    public class FeatureToggleService : Orc.FeatureToggles.IFeatureToggleService
    {
        public FeatureToggleService() { }
    }
    public interface IFeatureToggleSerializationService { }
    public interface IFeatureToggleService { }
}