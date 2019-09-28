[assembly: System.Resources.NeutralResourcesLanguageAttribute("en-US")]
[assembly: System.Runtime.Versioning.TargetFrameworkAttribute(".NETFramework,Version=v4.6", FrameworkDisplayName=".NET Framework 4.6")]
public class static ModuleInitializer
{
    public static void Initialize() { }
}
namespace Orc.FeatureToggles
{
    public class FeatureToggle : Catel.Data.ModelBase
    {
        public static readonly Catel.Data.PropertyData DefaultValueProperty;
        public static readonly Catel.Data.PropertyData DescriptionProperty;
        public static readonly Catel.Data.PropertyData IsHiddenProperty;
        public static readonly Catel.Data.PropertyData NameProperty;
        public FeatureToggle() { }
        public bool DefaultValue { get; set; }
        public string Description { get; set; }
        public bool IsHidden { get; set; }
        public string Name { get; set; }
        public bool Value { get; set; }
        public event System.EventHandler<Orc.FeatureToggles.ToggledEventArgs> Toggled;
        protected override void OnPropertyChanged(Catel.Data.AdvancedPropertyChangedEventArgs e) { }
        protected void RaiseToggled(bool oldValue, bool newValue) { }
        public override string ToString() { }
    }
    public class static FeatureToggleExtensions
    {
        public static void Toggle(this Orc.FeatureToggles.FeatureToggle toggle) { }
    }
    public class FeatureToggleSerializationService : Orc.FeatureToggles.IFeatureToggleSerializationService
    {
        public FeatureToggleSerializationService(Orc.FileSystem.IDirectoryService directoryService, Orc.FileSystem.IFileService fileService, Catel.Runtime.Serialization.Xml.IXmlSerializer xmlSerializer) { }
        protected virtual string GetFileName() { }
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Orc.FeatureToggles.FeatureToggle>> LoadAsync() { }
        public System.Threading.Tasks.Task SaveAsync(System.Collections.Generic.List<Orc.FeatureToggles.FeatureToggle> featureToggles) { }
    }
    public class FeatureToggleService : Orc.FeatureToggles.IFeatureToggleService
    {
        public FeatureToggleService(Orc.FeatureToggles.IFeatureToggleSerializationService featureToggleSerializationService) { }
        public event System.EventHandler<System.EventArgs> Loaded;
        public event System.EventHandler<System.EventArgs> Saved;
        public event System.EventHandler<Orc.FeatureToggles.ToggleEventArgs> ToggleAdded;
        public event System.EventHandler<Orc.FeatureToggles.ToggledEventArgs> Toggled;
        public event System.EventHandler<Orc.FeatureToggles.ToggleEventArgs> ToggleRemoved;
        public bool AddToggle(Orc.FeatureToggles.FeatureToggle toggle) { }
        public Orc.FeatureToggles.FeatureToggle GetToggle(string name) { }
        public System.Collections.Generic.IEnumerable<Orc.FeatureToggles.FeatureToggle> GetToggles() { }
        public System.Threading.Tasks.Task LoadAsync() { }
        public bool RemoveToggle(Orc.FeatureToggles.FeatureToggle toggle) { }
        public System.Threading.Tasks.Task SaveAsync() { }
    }
    public interface IFeatureToggleSerializationService
    {
        System.Threading.Tasks.Task<System.Collections.Generic.List<Orc.FeatureToggles.FeatureToggle>> LoadAsync();
        System.Threading.Tasks.Task SaveAsync(System.Collections.Generic.List<Orc.FeatureToggles.FeatureToggle> featureToggles);
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
        System.Threading.Tasks.Task LoadAsync();
        bool RemoveToggle(Orc.FeatureToggles.FeatureToggle toggle);
        System.Threading.Tasks.Task SaveAsync();
    }
    public class static IFeatureToggleServiceExtensions
    {
        public static System.Nullable<bool> GetValue(this Orc.FeatureToggles.IFeatureToggleService service, string name) { }
        public static bool RemoveToggle(this Orc.FeatureToggles.IFeatureToggleService service, string name) { }
        public static bool Toggle(this Orc.FeatureToggles.IFeatureToggleService service, string name) { }
    }
    public class ToggledEventArgs : System.EventArgs
    {
        public ToggledEventArgs(Orc.FeatureToggles.FeatureToggle toggle, bool oldValue, bool newValue) { }
        public bool NewValue { get; }
        public bool OldValue { get; }
        public Orc.FeatureToggles.FeatureToggle Toggle { get; }
    }
    public class ToggleEventArgs : System.EventArgs
    {
        public ToggleEventArgs(Orc.FeatureToggles.FeatureToggle toggle) { }
        public Orc.FeatureToggles.FeatureToggle Toggle { get; }
    }
}