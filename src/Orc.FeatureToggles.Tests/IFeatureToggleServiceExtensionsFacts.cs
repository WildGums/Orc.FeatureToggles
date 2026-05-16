namespace Orc.FeatureToggles.Tests;

using System;
using Catel.IoC;
using Catel.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

public class IFeatureToggleServiceExtensionsFacts
{
    [TestFixture]
    public class The_GetRequiredToggle_Method
    {
        [TestCase]
        public void Returns_Toggle_For_Existing_Name()
        {
            var service = CreateService();
            var toggle = new FeatureToggle
            {
                Name = "my-toggle"
            };

            service.AddToggle(toggle);

            var result = service.GetRequiredToggle(toggle.Name);

            Assert.That(result, Is.SameAs(toggle));
        }

        [TestCase]
        public void Throws_Localized_Message_For_Missing_Name()
        {
            var service = CreateService();
            var languageService = IoCContainer.ServiceProvider.GetRequiredService<ILanguageService>();

            var exception = Assert.Throws<InvalidOperationException>(() => service.GetRequiredToggle("missing"));

            Assert.That(exception, Is.Not.Null);
            Assert.That(exception!.Message, Is.EqualTo(languageService.GetRequiredStringAndFormat(
                "Orc_FeatureToggles_IFeatureToggleServiceExtensions_RequiredToggleNotFound",
                "missing")));
        }

        [TestCase]
        public void Falls_Back_To_Default_Message_When_Language_Service_Is_Unavailable()
        {
            var service = CreateService();
            var originalServiceProvider = IoCContainer.ServiceProvider;

#pragma warning disable IDISP001
            using var serviceProvider = new ServiceCollection().BuildServiceProvider();
#pragma warning restore IDISP001

            IoCContainer.ServiceProvider = serviceProvider;

            try
            {
                var exception = Assert.Throws<InvalidOperationException>(() => service.GetRequiredToggle("missing"));

                Assert.That(exception, Is.Not.Null);
                Assert.That(exception!.Message, Is.EqualTo("Could not find required toggle 'missing'"));
            }
            finally
            {
                IoCContainer.ServiceProvider = originalServiceProvider;
            }
        }
    }

    private static IFeatureToggleService CreateService()
    {
        var logger = NullLogger<FeatureToggleSerializationService>.Instance;
        var featureToggleInitializationService = Mock.Of<IFeatureToggleInitializationService>();
        var featureToggleSerializationService = Mock.Of<IFeatureToggleSerializationService>();

        return new FeatureToggleService(logger, featureToggleInitializationService, featureToggleSerializationService);
    }
}
