namespace Orc.FeatureToggles.Tests
{
    using System.Runtime.CompilerServices;
    using ApiApprover;
    using NUnit.Framework;
    using Views;

    [TestFixture]
    public class PublicApiFacts
    {
        [Test, MethodImpl(MethodImplOptions.NoInlining)]
        public void Orc_FeatureToggles_HasNoBreakingChanges()
        {
            var assembly = typeof(FeatureToggle).Assembly;

            PublicApiApprover.ApprovePublicApi(assembly);
        }

        [Test, MethodImpl(MethodImplOptions.NoInlining)]
        public void Orc_FeatureToggles_Xaml_HasNoBreakingChanges()
        {
            var assembly = typeof(ManageFeatureTogglesView).Assembly;

            PublicApiApprover.ApprovePublicApi(assembly);
        }
    }
}
