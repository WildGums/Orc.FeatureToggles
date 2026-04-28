namespace Orc.FeatureToggles.Tests.Services;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

public class FeatureToggleServiceFacts
{
    [TestFixture]
    public class The_Toggle_Method
    {
        [TestCase]
        public void Raises_Toggled_Event()
        {
            var service = CreateService();

            var toggle = new FeatureToggle
            {
                Name = "My toggle",
            };

            service.AddToggle(toggle);

            var success = false;

            service.Toggled += (_, _) =>
            {
                success = true;
            };

            service.Toggle("My toggle");

            Assert.That(success, Is.True);
        }
    }

    [TestFixture]
    public class The_LoadAsync_Method
    {
        [TestCase]
        public async Task Should_Not_Save_During_Load()
        {
            var toggle1 = new FeatureToggle
            {
                Name = "My toggle 1",
                Value = false
            };

            var toggle2 = new FeatureToggle
            {
                Name = "My toggle 2",
                Value = false
            };

            var calledSave = false;

            var featureToggleSerializationServiceMock = new Mock<IFeatureToggleSerializationService>();
            featureToggleSerializationServiceMock.Setup(x => x.LoadAsync())
                .Returns(async () =>
                {
                    return new[]
                    {
                        new FeatureToggleValue
                        {
                            Name = "My toggle 1",
                            Value = true
                        }
                    };
                });

            featureToggleSerializationServiceMock.Setup(x => x.SaveAsync(It.IsAny<IReadOnlyList<FeatureToggleValue>>()))
                .Callback<IEnumerable<FeatureToggleValue>>(_ =>
                {
                    calledSave = true;
                });

            var service = CreateService(featureToggleSerializationServiceMock);
            service.AddToggle(toggle1);
            service.AddToggle(toggle2);

            await service.InitializeAndLoadAsync();

            Assert.That(calledSave, Is.False);
        }

        [TestCase]
        public async Task Raised_Loaded_Event()
        {
            var service = CreateService();

            var success = false;

            service.Loaded += (_, _) =>
            {
                success = true;
            };

            await service.LoadAsync();

            Assert.That(success, Is.True);
        }
    }

    [TestFixture]
    public class The_SaveAsync_Method
    {
        [TestCase]
        public async Task Raised_Saved_Event()
        {
            var service = CreateService();

            var success = false;

            service.Saved += (_, _) =>
            {
                success = true;
            };

            await service.SaveAsync();

            Assert.That(success, Is.True);
        }
    }

    [TestFixture]
    public class The_AddToggle_Method
    {
        [TestCase]
        public void Raises_ToggleAdded_Event()
        {
            var service = CreateService();
            var success = false;

            service.ToggleAdded += (_, _) =>
            {
                success = true;
            };

            var toggle = new FeatureToggle
            {
                Name = "My toggle",
            };

            service.AddToggle(toggle);

            Assert.That(success, Is.True);
        }
    }

    [TestFixture]
    public class The_RemoveToggle_Method
    {
        [TestCase]
        public void Returns_True_For_Existing_Toggle()
        {
            var service = CreateService();

            var toggle = new FeatureToggle
            {
                Name = "My toggle",
            };

            service.AddToggle(toggle);

            Assert.That(service.RemoveToggle("My toggle"), Is.True);
        }

        [TestCase]
        public void Returns_False_For_Non_Existing_Toggle()
        {
            var service = CreateService();

            var toggle = new FeatureToggle
            {
                Name = "My toggle",
            };

            service.AddToggle(toggle);

            Assert.That(service.RemoveToggle("non existing toggle"), Is.False);
        }

        [TestCase]
        public void Raises_ToggleRemoved_Event()
        {
            var service = CreateService();

            var toggle = new FeatureToggle
            {
                Name = "My toggle",
            };

            service.AddToggle(toggle);

            var success = false;

            service.ToggleRemoved += (_, _) =>
            {
                success = true;
            };

            service.RemoveToggle(toggle);

            Assert.That(success, Is.True);
        }
    }

    private static IFeatureToggleService CreateService(Mock<IFeatureToggleSerializationService> featureToggleSerializationServiceMock = null)
    {
        var serviceCollection = ServiceCollectionHelper.CreateServiceCollection();

        if (featureToggleSerializationServiceMock is null)
        {
            featureToggleSerializationServiceMock = new Mock<IFeatureToggleSerializationService>();
            featureToggleSerializationServiceMock.Setup(x => x.LoadAsync())
                .Returns(async () =>
                {
                    return Array.Empty<FeatureToggleValue>();
                });
        }

        serviceCollection.AddSingleton<IFeatureToggleSerializationService>(featureToggleSerializationServiceMock.Object);

#pragma warning disable IDISP001 // Dispose created
        var provider = serviceCollection.BuildServiceProvider();
#pragma warning restore IDISP001 // Dispose created

        return ActivatorUtilities.CreateInstance<FeatureToggleService>(provider);
    }
}
