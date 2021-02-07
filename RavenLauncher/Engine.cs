using System;
using Log5RLibs.Services;

using static RavenLauncher.Schemes.LauncherScheme;

namespace RavenLauncher {
    public static class Engine {
        public static void Main(string[] args) {
            // Start Message
            AlConsole.WriteLine(LauncherInfoScheme, $"Start Up RavenLauncher.");
            
            // Arguments Parse.
            ArgParser.Decomposition(args);
            
            // Make Status Folder
            StatusChecker.MakeStatusDir();
            
            // Setup
            if (!StatusChecker.IsDownloaded()) {
                RavenSetupProcess.BuildController(Settings.CloneDir);
                Settings.RecvFile.Create();
            }
            
            // Service Starter
            ScheduledTasks.Fire();
            AlConsole.WriteLine(LauncherCautScheme, "> Press Enter, Launcher to Stop. <");
            Console.ReadLine();
            AlConsole.WriteLine(LauncherCautScheme, "Stopped Launcher.");
        }
    }
}