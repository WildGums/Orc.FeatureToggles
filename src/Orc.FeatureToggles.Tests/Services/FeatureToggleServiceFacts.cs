namespace Orc.FeatureToggles.Tests.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using Catel.IoC;
using Moq;
using NUnit.Framework;

public class FeatureToggleServiceFacts
{
    private static IFeatureToggleService CreateService(Mock<IFeatureToggleSerializationService> featureToggleSerializationServiceMock = null)
    {
        if (featureToggleSerializationServiceMock is not null)
        {
            return new FeatureToggleService(new FeatureToggleInitializationService(TypeFactory.Default),
                featureToggleSerializationServiceMock.Object);
        }

        featureToggleSerializationServiceMock = new Mock<IFeatureToggleSerializationService>();
        featureToggleSerializationServiceMock.Setup(x => x.LoadAsync())
            .Returns(async () =>
            {
                return new FeatureToggleValue[] { };
            });

        return new FeatureToggleService(new FeatureToggleInitializationService(TypeFactory.Default),
            featureToggleSerializationServiceMock.Object);
    }

    [TestFixture]
    public class TheToggleMethod
    {
        [TestCase]
        public void RaisesToggledEvent()
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
    public class TheLoadAsyncMethod
    {
        [TestCase]
        public async Task Should_Not_Save_During_Load_Async()
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

            featureToggleSerializationServiceMock.Setup(x => x.SaveAsync(It.IsAny<IEnumerable<FeatureToggleValue>>()))
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
        public async Task RaisedLoadedEventAsync()
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
    public class TheSaveAsyncMethod
    {
        [TestCase]
        public async Task RaisedSavedEventAsync()
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
    public class TheAddToggleMethod
    {
        [TestCase]
        public void RaisesToggleAddedEvent()
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
    public class TheRemoveToggleMethod
    {
        [TestCase]
        public void ReturnsTrueForExistingToggle()
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
        public void ReturnsFalseForNonExistingToggle()
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
        public void RaisesToggleRemovedEvent()
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
}
