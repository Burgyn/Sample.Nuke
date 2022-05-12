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

    bool CanExecute = true;

    Target A => _ => _
        .Triggers(B)
        .Executes(() =>
        {
            CanExecute = false;
            Log.Logger.Information("Target A");
        });

    Target B => _ => _
        .Triggers(C, D)
        .OnlyWhenDynamic(()=> CanExecute)
        .Executes(() =>
        {
            Log.Logger.Information("Target B");
        });

    Target C => _ => _
        .OnlyWhenDynamic(()=> CanExecute)
        .Executes(() =>
        {
            Log.Logger.Information("Target C");
        });
    
    Target D => _ => _
        .OnlyWhenDynamic(()=> CanExecute)
        .Executes(() =>
        {
            Log.Logger.Information("Target D");
        });
}
