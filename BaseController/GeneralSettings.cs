using System;

namespace BaseController {
    public static class GeneralSettings {
        public static readonly string ConfigFileName = "raven_config.yaml";
        
        private static readonly OperatingSystem OPSystem = Environment.OSVersion;
        public static readonly string BaseDirectory      = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly int    GeneralPaddingNum  = 16;
        public static readonly bool   IsLinux            = OPSystem.Platform != PlatformID.Win32NT;
        
        // Config Dir
        private static readonly string WinConfigDir   = AppDomain.CurrentDomain.BaseDirectory + "Config\\";
        private static readonly string LinuxConfigDir = AppDomain.CurrentDomain.BaseDirectory + "Config/";
        public  static readonly string ConfigDir      = GeneralSettings.IsLinux ? LinuxConfigDir : WinConfigDir;
    }
}