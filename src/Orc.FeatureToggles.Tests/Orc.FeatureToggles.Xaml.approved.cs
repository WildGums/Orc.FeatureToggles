[assembly: System.Resources.NeutralResourcesLanguageAttribute("en-US")]
[assembly: System.Runtime.Versioning.TargetFrameworkAttribute(".NETFramework,Version=v4.6", FrameworkDisplayName=".NET Framework 4.6")]
[assembly: System.Windows.Markup.XmlnsDefinitionAttribute("http://schemas.wildgums.com/orc/featuretoggles", "Orc.FeatureToggles")]
[assembly: System.Windows.Markup.XmlnsDefinitionAttribute("http://schemas.wildgums.com/orc/featuretoggles", "Orc.FeatureToggles.Views")]
[assembly: System.Windows.Markup.XmlnsPrefixAttribute("http://schemas.wildgums.com/orc/featuretoggles", "orcfeaturetoggles")]
[assembly: System.Windows.ThemeInfoAttribute(System.Windows.ResourceDictionaryLocation.None, System.Windows.ResourceDictionaryLocation.SourceAssembly)]
public class static ModuleInitializer
{
    public static void Initialize() { }
}
namespace Orc.FeatureToggles.ViewModels
{
    public class ManageFeatureTogglesViewModel : Catel.MVVM.ViewModelBase
    {
        public static readonly Catel.Data.PropertyData SelectedToggleProperty;
        public static readonly Catel.Data.PropertyData ToggleFilterProperty;
        public static readonly Catel.Data.PropertyData TogglesProperty;
        public ManageFeatureTogglesViewModel(Orc.FeatureToggles.IFeatureToggleService featureToggleService, Catel.MVVM.ICommandManager commandManager, Catel.Services.ILanguageService languageService, Catel.Services.IMessageService messageService) { }
        public Orc.FeatureToggles.FeatureToggle SelectedToggle { get; set; }
        public override string Title { get; }
        public Catel.MVVM.Command Toggle { get; }
        public string ToggleFilter { get; set; }
        public Catel.Collections.FastObservableCollection<Orc.FeatureToggles.FeatureToggle> Toggles { get; }
        protected override System.Threading.Tasks.Task CloseAsync() { }
        protected override System.Threading.Tasks.Task InitializeAsync() { }
    }
}
namespace Orc.FeatureToggles.Views
{
    public class ManageFeatureTogglesView : Catel.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
    {
        public ManageFeatureTogglesView() { }
        public void InitializeComponent() { }
    }
    public class ManageFeatureTogglesWindow : Catel.Windows.DataWindow, System.Windows.Markup.IComponentConnector
    {
        public ManageFeatureTogglesWindow() { }
        public ManageFeatureTogglesWindow(Orc.FeatureToggles.ViewModels.ManageFeatureTogglesViewModel viewModel) { }
        public void InitializeComponent() { }
    }
}