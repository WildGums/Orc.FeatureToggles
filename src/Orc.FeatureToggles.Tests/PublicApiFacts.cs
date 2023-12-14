namespace Orc.FeatureToggles.Tests;

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NUnit.Framework;
using PublicApiGenerator;
using VerifyNUnit;
using Views;

[TestFixture]
public class PublicApiFacts
{
    [Test, MethodImpl(MethodImplOptions.NoInlining)]
    public async Task Orc_FeatureToggles_HasNoBreakingChanges_Async()
    {
        var assembly = typeof(FeatureToggle).Assembly;

        await PublicApiApprover.ApprovePublicApiAsync(assembly);
    }

    [Test, MethodImpl(MethodImplOptions.NoInlining)]
    public async Task Orc_FeatureToggles_Xaml_HasNoBreakingChanges_Async()
    {
        var assembly = typeof(ManageFeatureTogglesView).Assembly;

        await PublicApiApprover.ApprovePublicApiAsync(assembly);
    }

    internal static class PublicApiApprover
    {
        public static async Task ApprovePublicApiAsync(Assembly assembly)
        {
            var publicApi = ApiGenerator.GeneratePublicApi(assembly, new ApiGeneratorOptions());
            await Verifier.Verify(publicApi);
        }
    }
}
