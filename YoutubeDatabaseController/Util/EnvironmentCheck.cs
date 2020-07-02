using System;

namespace YoutubeDatabaseController.Util {
    public static class EnvironmentCheck {
        public static bool IsLinux() {
            return !Environment.OSVersion.Platform.ToString().Equals("Win32NT");
        }
    }
}