namespace Orc.FeatureToggles.Tests.Models;

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

[TestFixture]
public class FeatureToggleFacts
{
    [Test]
    [TestCase]
    public static void CanBeOrdered()
    {
        var toggles = new List<FeatureToggle>
        {
            new FeatureToggle(),
            new FeatureToggle(),
            new DummyFeatureToggle(),
            new DummyFeatureToggle()
        };

        toggles = toggles.OrderBy(x => x).ToList();
    }

    private class DummyFeatureToggle : FeatureToggle
    {
        public DummyFeatureToggle()
        {
            Name = nameof(DummyFeatureToggle);
        }
    }
}
