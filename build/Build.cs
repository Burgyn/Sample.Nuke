using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities.Collections;
using Serilog;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;

class Build : NukeBuild
{
    public static int Main () => Execute<Build>(x => x.A);

    Target A => _ => _
        .Triggers(B)
        .Executes(() =>
        {
            foreach (var target in ExecutionPlan)
            {
                target.Status = ExecutionStatus.Skipped;
            }
            Log.Logger.Information("Target A");
        });

    Target B => _ => _
        .Triggers(C, D)
        .Executes(() =>
        {
            Log.Logger.Information("Target B");
        });

    Target C => _ => _
        .Executes(() =>
        {
            Log.Logger.Information("Target C");
        });
    
    Target D => _ => _
        .Executes(() =>
        {
            Log.Logger.Information("Target D");
        });
}
