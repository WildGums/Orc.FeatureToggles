[assembly: System.Resources.NeutralResourcesLanguageAttribute("en-US")]
[assembly: System.Runtime.Versioning.TargetFrameworkAttribute(".NETFramework,Version=v4.6", FrameworkDisplayName=".NET Framework 4.6")]
public class static ModuleInitializer
{
    public static void Initialize() { }
}
namespace Orc.FeatureToggles
{
    public class FeatureToggle : Catel.Data.ObservableObject
    {
        public FeatureToggle() { }
        public bool DefaultValue { get; set; }
        public string Description { get; set; }
        public bool IsHidden { get; set; }
        public string Name { get; set; }
        public System.Nullable<bool> Value { get; set; }
        public event System.EventHandler<Orc.FeatureToggles.ToggledEventArgs> Toggled;
        protected void RaiseToggled(System.Nullable<bool> oldValue, System.Nullable<bool> newValue) { }
        public override string ToString() { }
    }
    public class static FeatureToggleExtensions
    {
        public static void Toggle(this Orc.FeatureToggles.FeatureToggle toggle) { }
    }
    public class FeatureToggleInitializationService : Orc.FeatureToggles.IFeatureToggleInitializationService
    {
        public FeatureToggleInitializationService(Catel.IoC.ITypeFactory typeFactory) { }
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Orc.FeatureToggles.FeatureToggle>> FindTogglesAsync() { }
    }
    public class FeatureToggleSerializationService : Orc.FeatureToggles.IFeatureToggleSerializationService
    {
        public FeatureToggleSerializationService(Orc.FileSystem.IDirectoryService directoryService, Orc.FileSystem.IFileService fileService, Catel.Runtime.Serialization.Xml.IXmlSerializer xmlSerializer) { }
        protected virtual string GetFileName() { }
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Orc.FeatureToggles.FeatureToggleValue>> LoadAsync() { }
        public System.Threading.Tasks.Task SaveAsync(System.Collections.Generic.List<Orc.FeatureToggles.FeatureToggleValue> toggleValues) { }
    }
    public class FeatureToggleService : Orc.FeatureToggles.IFeatureToggleService
    {
        public FeatureToggleService(Orc.FeatureToggles.IFeatureToggleInitializationService featureToggleInitializationService, Orc.FeatureToggles.IFeatureToggleSerializationService featureToggleSerializationService) { }
        public event System.EventHandler<System.EventArgs> Loaded;
        public event System.EventHandler<System.EventArgs> Saved;
        public event System.EventHandler<Orc.FeatureToggles.ToggleEventArgs> ToggleAdded;
        public event System.EventHandler<Orc.FeatureToggles.ToggledEventArgs> Toggled;
        public event System.EventHandler<Orc.FeatureToggles.ToggleEventArgs> ToggleRemoved;
        public bool AddToggle(Orc.FeatureToggles.FeatureToggle toggle) { }
        public Orc.FeatureToggles.FeatureToggle GetToggle(string name) { }
        public System.Collections.Generic.IEnumerable<Orc.FeatureToggles.FeatureToggle> GetToggles() { }
        public System.Threading.Tasks.Task InitializeAsync() { }
        public System.Threading.Tasks.Task LoadAsync() { }
        public bool RemoveToggle(Orc.FeatureToggles.FeatureToggle toggle) { }
        public System.Threading.Tasks.Task SaveAsync() { }
    }
    public class FeatureToggleValue : Catel.Data.ModelBase
    {
        public static readonly Catel.Data.PropertyData NameProperty;
        public static readonly Catel.Data.PropertyData ValueProperty;
        public FeatureToggleValue() { }
        public FeatureToggleValue(Orc.FeatureToggles.FeatureToggle toggle) { }
        public string Name { get; set; }
        public System.Nullable<bool> Value { get; set; }
    }
    public interface IFeatureToggleInitializationService
    {
        System.Threading.Tasks.Task<System.Collections.Generic.List<Orc.FeatureToggles.FeatureToggle>> FindTogglesAsync();
    }
    public interface IFeatureToggleProvider
    {
        System.Threading.Tasks.Task<System.Collections.Generic.List<Orc.FeatureToggles.FeatureToggle>> ProvideTogglesAsync();
    }
    public interface IFeatureToggleSerializationService
    {
        System.Threading.Tasks.Task<System.Collections.Generic.List<Orc.FeatureToggles.FeatureToggleValue>> LoadAsync();
        System.Threading.Tasks.Task SaveAsync(System.Collections.Generic.List<Orc.FeatureToggles.FeatureToggleValue> toggleValues);
    }
    public interface IFeatureToggleService
    {
        public event System.EventHandler<System.EventArgs> Loaded;
        public event System.EventHandler<System.EventArgs> Saved;
        public event System.EventHandler<Orc.FeatureToggles.ToggleEventArgs> ToggleAdded;
        public event System.EventHandler<Orc.FeatureToggles.ToggledEventArgs> Toggled;
        public event System.EventHandler<Orc.FeatureToggles.ToggleEventArgs> ToggleRemoved;
        bool AddToggle(Orc.FeatureToggles.FeatureToggle toggle);
        Orc.FeatureToggles.FeatureToggle GetToggle(string name);
        System.Collections.Generic.IEnumerable<Orc.FeatureToggles.FeatureToggle> GetToggles();
        System.Threading.Tasks.Task InitializeAsync();
        System.Threading.Tasks.Task LoadAsync();
        bool RemoveToggle(Orc.FeatureToggles.FeatureToggle toggle);
        System.Threading.Tasks.Task SaveAsync();
    }
    public class static IFeatureToggleServiceExtensions
    {
        public static System.Nullable<bool> GetValue(this Orc.FeatureToggles.IFeatureToggleService service, string name) { }
        public static System.Threading.Tasks.Task InitializeAndLoadAsync(this Orc.FeatureToggles.IFeatureToggleService service) { }
        public static bool RemoveToggle(this Orc.FeatureToggles.IFeatureToggleService service, string name) { }
        public static bool Toggle(this Orc.FeatureToggles.IFeatureToggleService service, string name) { }
    }
    public class ToggledEventArgs : Orc.FeatureToggles.ToggleEventArgs
    {
        public ToggledEventArgs(Orc.FeatureToggles.FeatureToggle toggle, System.Nullable<bool> oldValue, System.Nullable<bool> newValue) { }
        public System.Nullable<bool> NewValue { get; }
        public System.Nullable<bool> OldValue { get; }
    }
    public class ToggleEventArgs : System.EventArgs
    {
        public ToggleEventArgs(Orc.FeatureToggles.FeatureToggle toggle) { }
        public Orc.FeatureToggles.FeatureToggle Toggle { get; }
        public bool IsToggle(string name) { }
    }
}