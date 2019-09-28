namespace Orc.FeatureToggles.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;

    public class FeatureToggleServiceFacts
    {
        private static IFeatureToggleService CreateService()
        {
            return new FeatureToggleService(new EmptyFeatureToggleSerializationService());
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
