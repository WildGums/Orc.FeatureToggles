[assembly: System.Resources.NeutralResourcesLanguage("en-US")]
[assembly: System.Runtime.Versioning.TargetFramework(".NETCoreApp,Version=v6.0", FrameworkDisplayName="")]
public static class ModuleInitializer
{
    public static void Initialize() { }
}
namespace Orc.FeatureToggles
{
    public class FeatureToggle : Catel.Data.ObservableObject, System.IComparable, System.IComparable<Orc.FeatureToggles.FeatureToggle>
    {
        public FeatureToggle() { }
        public bool DefaultValue { get; set; }
        public string Description { get; set; }
        public bool EffectiveValue { get; }
        public bool IsHidden { get; set; }
        public string Name { get; set; }
        public bool? Value { get; set; }
        public event System.EventHandler<Orc.FeatureToggles.ToggledEventArgs>? Toggled;
        public int CompareTo(Orc.FeatureToggles.FeatureToggle? other) { }
        public int CompareTo(object? obj) { }
        protected override void OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e) { }
        protected void RaiseToggled(bool? oldValue, bool? newValue) { }
        public override string ToString() { }
    }
    public static class FeatureToggleExtensions
    {
        public static void Reset(this Orc.FeatureToggles.FeatureToggle toggle) { }
        public static void Toggle(this Orc.FeatureToggles.FeatureToggle toggle) { }
    }
    public class FeatureToggleInitializationService : Orc.FeatureToggles.IFeatureToggleInitializationService
    {
        public FeatureToggleInitializationService(Catel.IoC.ITypeFactory typeFactory) { }
        public System.Threading.Tasks.Task<Orc.FeatureToggles.FeatureToggle[]> FindTogglesAsync() { }
    }
    public class FeatureToggleSerializationService : Orc.FeatureToggles.IFeatureToggleSerializationService
    {
        public FeatureToggleSerializationService(Orc.FileSystem.IDirectoryService directoryService, Orc.FileSystem.IFileService fileService, Catel.Runtime.Serialization.Xml.IXmlSerializer xmlSerializer, Catel.Services.IAppDataService appDataService) { }
        protected virtual string GetFileName() { }
        public System.Threading.Tasks.Task<Orc.FeatureToggles.FeatureToggleValue[]> LoadAsync() { }
        public System.Threading.Tasks.Task SaveAsync(System.Collections.Generic.IEnumerable<Orc.FeatureToggles.FeatureToggleValue> toggleValues) { }
    }
    public class FeatureToggleService : Orc.FeatureToggles.IFeatureToggleService
    {
        public FeatureToggleService(Orc.FeatureToggles.IFeatureToggleInitializationService featureToggleInitializationService, Orc.FeatureToggles.IFeatureToggleSerializationService featureToggleSerializationService) { }
        public event System.EventHandler<System.EventArgs>? Loaded;
        public event System.EventHandler<System.EventArgs>? Saved;
        public event System.EventHandler<Orc.FeatureToggles.ToggleEventArgs>? ToggleAdded;
        public event System.EventHandler<Orc.FeatureToggles.ToggleEventArgs>? ToggleRemoved;
        public event System.EventHandler<Orc.FeatureToggles.ToggledEventArgs>? Toggled;
        public bool AddToggle(Orc.FeatureToggles.FeatureToggle toggle) { }
        public Orc.FeatureToggles.FeatureToggle? GetToggle(string name) { }
        public Orc.FeatureToggles.FeatureToggle[] GetToggles() { }
        public System.Threading.Tasks.Task InitializeAsync() { }
        public System.Threading.Tasks.Task LoadAsync() { }
        public bool RemoveToggle(Orc.FeatureToggles.FeatureToggle toggle) { }
        public System.Threading.Tasks.Task SaveAsync() { }
    }
    public class FeatureToggleValue : Catel.Data.ModelBase
    {
        public static readonly Catel.Data.IPropertyData NameProperty;
        public static readonly Catel.Data.IPropertyData ValueProperty;
        public FeatureToggleValue() { }
        public FeatureToggleValue(Orc.FeatureToggles.FeatureToggle toggle) { }
        public string Name { get; set; }
        public bool? Value { get; set; }
    }
    public interface IFeatureToggleInitializationService
    {
        System.Threading.Tasks.Task<Orc.FeatureToggles.FeatureToggle[]> FindTogglesAsync();
    }
    public interface IFeatureToggleProvider
    {
        System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Orc.FeatureToggles.FeatureToggle>> ProvideTogglesAsync();
    }
    public interface IFeatureToggleSerializationService
    {
        System.Threading.Tasks.Task<Orc.FeatureToggles.FeatureToggleValue[]> LoadAsync();
        System.Threading.Tasks.Task SaveAsync(System.Collections.Generic.IEnumerable<Orc.FeatureToggles.FeatureToggleValue> toggleValues);
    }
    public interface IFeatureToggleService
    {
        event System.EventHandler<System.EventArgs>? Loaded;
        event System.EventHandler<System.EventArgs>? Saved;
        event System.EventHandler<Orc.FeatureToggles.ToggleEventArgs>? ToggleAdded;
        event System.EventHandler<Orc.FeatureToggles.ToggleEventArgs>? ToggleRemoved;
        event System.EventHandler<Orc.FeatureToggles.ToggledEventArgs>? Toggled;
        bool AddToggle(Orc.FeatureToggles.FeatureToggle toggle);
        Orc.FeatureToggles.FeatureToggle? GetToggle(string name);
        Orc.FeatureToggles.FeatureToggle[] GetToggles();
        System.Threading.Tasks.Task InitializeAsync();
        System.Threading.Tasks.Task LoadAsync();
        bool RemoveToggle(Orc.FeatureToggles.FeatureToggle toggle);
        System.Threading.Tasks.Task SaveAsync();
    }
    public static class IFeatureToggleServiceExtensions
    {
        public static Orc.FeatureToggles.FeatureToggle GetRequiredToggle(this Orc.FeatureToggles.IFeatureToggleService service, string name) { }
        public static bool GetValue(this Orc.FeatureToggles.IFeatureToggleService service, string name, bool fallbackValue) { }
        public static System.Threading.Tasks.Task InitializeAndLoadAsync(this Orc.FeatureToggles.IFeatureToggleService service) { }
        public static bool RemoveToggle(this Orc.FeatureToggles.IFeatureToggleService service, string name) { }
        public static bool Toggle(this Orc.FeatureToggles.IFeatureToggleService service, string name) { }
    }
    public class ToggleEventArgs : System.EventArgs
    {
        public ToggleEventArgs(Orc.FeatureToggles.FeatureToggle toggle) { }
        public Orc.FeatureToggles.FeatureToggle Toggle { get; }
        public bool IsToggle(string name) { }
    }
    public class ToggledEventArgs : Orc.FeatureToggles.ToggleEventArgs
    {
        public ToggledEventArgs(Orc.FeatureToggles.FeatureToggle toggle, bool? oldValue, bool? newValue) { }
        public bool? NewValue { get; }
        public bool? OldValue { get; }
    }
}