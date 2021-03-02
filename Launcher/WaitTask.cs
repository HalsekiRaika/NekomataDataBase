using System;
using Log5RLibs.Services;
using static Launcher.Schemes.LauncherScheme;

namespace Launcher {
    public static class WaitTask {
        public static void onStart() {
            while (true) {
                string stdin = Console.ReadLine();
                // ReSharper disable once PossibleNullReferenceException
                if (stdin.Equals("reload")) {
                    ScheduledTasks.onReload();
                }
                if (stdin.Equals("stop")) {
                    AlConsole.WriteLine(LauncherCautScheme, "Stopped Launcher.");
                    break;
                }
            }
        }
    }
}