using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Markup;

// All other assembly info is defined in SolutionAssemblyInfo.cs

[assembly: AssemblyTitle("Orc.FeatureToggles.Xaml")]
[assembly: AssemblyProduct("Orc.FeatureToggles.Xaml")]
[assembly: AssemblyDescription("Orc.FeatureToggles.Xaml library")]
[assembly: NeutralResourcesLanguage("en-US")]

[assembly: XmlnsPrefix("http://schemas.wildgums.com/orc/featuretoggles", "orcfeaturetoggles")]
[assembly: XmlnsDefinition("http://schemas.wildgums.com/orc/featuretoggles", "Orc.FeatureToggles")]
//[assembly: XmlnsDefinition("http://schemas.wildgums.com/orc/featuretoggles", "Orc.FeatureToggles.Behaviors")]
//[assembly: XmlnsDefinition("http://schemas.wildgums.com/orc/featuretoggles", "Orc.FeatureToggles.Converters")]
[assembly: XmlnsDefinition("http://schemas.wildgums.com/orc/featuretoggles", "Orc.FeatureToggles.Views")]

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
    //(used if a resource is not found in the page, 
    // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
    //(used if a resource is not found in the page, 
    // app, or any theme specific resource dictionaries)
)]
