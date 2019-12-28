// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobalInitialization.approvaltests.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


using ApprovalTests.Reporters;

[assembly: UseReporter(typeof(DiffReporter))]

public static class TargetFrameworkResolver
{
    public const string Current =

#if NET45
            "NET45"
#elif NET46
            "NET46"
#elif NET47
            "NET47"
#else
            "Unknown"
#endif
        ;
}
