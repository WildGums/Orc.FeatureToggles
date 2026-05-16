namespace Orc.FeatureToggles.Tests;

using System;
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
        public void Throws_Default_Message_For_Missing_Name()
        {
            var service = CreateService();

            var exception = Assert.Throws<InvalidOperationException>(() => service.GetRequiredToggle("missing"));

            Assert.That(exception, Is.Not.Null);
            Assert.That(exception!.Message, Is.EqualTo("Could not find required toggle 'missing'"));
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
