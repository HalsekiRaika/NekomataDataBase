using System;

namespace YoutubeDatabaseController.Util {
    public class EnvironmentCheck {
        public static bool IsLinux() {
            return !Environment.OSVersion.Platform.ToString().Equals("Win32NT");
        }
    }
}