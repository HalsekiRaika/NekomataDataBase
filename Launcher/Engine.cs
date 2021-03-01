using System;
using Log5RLibs.Services;
using SetupLibs;
using SetupLibs.Model;
using static Launcher.Schemes.LauncherScheme;

namespace Launcher {
    public static class Engine {
        public static void Main(string[] args) {
            // Arguments Parse.
            ArgParser.Decomposition(args);
            
            // Config Setup
            if (!Settings.DoSkipConfigGenerate) {
                ConfigExporter.onTemplateGenerate();
            }

            ConfigModel config = ConfigImporter.onDeserialize();
            Settings.DataBaseUserName = config.DB_ACCESS_USERNAME;
            Settings.DataBasePassWord = config.DB_ACCESS_PASSWORD;
            Settings.IgnoreDataArray = string.Join(',', config.ignoreData);

            // Make Status Folder
            StatusChecker.MakeStatusDir();
            
            // Start Message
            AlConsole.WriteLine(LauncherInfoScheme, $"Start Up RavenLauncher.");
            
            ScheduledTasks.Fire();
            AlConsole.WriteLine(LauncherCautScheme, "> Press Enter, Launcher to Stop. <");
            Console.ReadLine();
            AlConsole.WriteLine(LauncherCautScheme, "Stopped Launcher.");
            
            /*
            // Arguments Parse.
            ArgParser.Decomposition(args);
        
            
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
            */
        }
    }
}