using System;

namespace YoutubeDatabaseController.Util {
    public class EnvironmentCheck {
        public static bool IsLinux() {
            OperatingSystem osVersion = Environment.OSVersion;
            if (osVersion.Platform.ToString().Equals("Win32NT")) {
                return false;
            } else {
                return true;
            }
        }
    }
}