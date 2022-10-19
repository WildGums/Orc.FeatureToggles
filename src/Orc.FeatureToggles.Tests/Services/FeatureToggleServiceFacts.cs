namespace Orc.FeatureToggles.Tests.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catel.IoC;
    using Moq;
    using NUnit.Framework;

    public class FeatureToggleServiceFacts
    {
        private static IFeatureToggleService CreateService(Mock<IFeatureToggleSerializationService> featureToggleSerializationServiceMock = null)
        {
            if (featureToggleSerializationServiceMock is null)
            {
                featureToggleSerializationServiceMock = new Mock<IFeatureToggleSerializationService>();
                featureToggleSerializationServiceMock.Setup(x => x.LoadAsync())
                    .Returns(async () =>
                    {
                        return new FeatureToggleValue[] { };
                    });
            }

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

                service.Toggled += (sender, e) =>
                {
                    success = true;
                };

                service.Toggle("My toggle");

                Assert.IsTrue(success);
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
                        return new FeatureToggleValue[]
                        {
                            new FeatureToggleValue
                            {
                                Name = "My toggle 1",
                                Value = true
                            }
                        };
                    });

                featureToggleSerializationServiceMock.Setup(x => x.SaveAsync(It.IsAny<IEnumerable<FeatureToggleValue>>()))
                    .Callback<IEnumerable<FeatureToggleValue>>(x =>
                    {
                        calledSave = true;
                    });

                var service = CreateService(featureToggleSerializationServiceMock);
                service.AddToggle(toggle1);
                service.AddToggle(toggle2);

                await service.InitializeAndLoadAsync();

                Assert.IsFalse(calledSave);
            }

            [TestCase]
            public async Task RaisedLoadedEventAsync()
            {
                var service = CreateService();

                var success = false;

                service.Loaded += (sender, e) =>
                {
                    success = true;
                };

                await service.LoadAsync();

                Assert.IsTrue(success);
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

                service.Saved += (sender, e) =>
                {
                    success = true;
                };

                await service.SaveAsync();

                Assert.IsTrue(success);
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

                service.ToggleAdded += (sender, e) =>
                {
                    success = true;
                };

                var toggle = new FeatureToggle
                {
                    Name = "My toggle",
                };

                service.AddToggle(toggle);

                Assert.IsTrue(success);
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

                Assert.IsTrue(service.RemoveToggle("My toggle"));
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

                Assert.IsFalse(service.RemoveToggle("non existing toggle"));
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

                service.ToggleRemoved += (sender, e) =>
                {
                    success = true;
                };

                service.RemoveToggle(toggle);

                Assert.IsTrue(success);
            }
        }
    }
}
