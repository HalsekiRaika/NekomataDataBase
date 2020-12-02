using System;

namespace YoutubeDatabaseController {
    public static class Settings {
        public static readonly string[] Startup = new string[] {
            "------------------ RUN START ------------------",
            $"{DateTime.Now:F}",
            "Tool was Created By ReiRokusanami",
            "-----------------------------------------------"
        };
        
        // Startup Time
        public static DateTime StartupTime { get; set; }

        // Use Quota Count and so on
        public static int UseQuota = 0;
        public static int CautData = 0;
        public static int WarnData = 0;
        
        // Production Names
        public static readonly string Hololive = "Hololive";
        public static readonly string Nijisanji = "Nijisanji";
        public static readonly string AniMare = "Animare";
        
        // Server
        public static readonly string NekomataAws   = "18.210.220.130";
        public static readonly string NekomataLocal = "192.168.0.5";

        // Config Dir
        private static readonly OperatingSystem OperatingSystem = Environment.OSVersion;
        public  static readonly bool   IsLinux        = OperatingSystem.Platform != PlatformID.Win32NT;
        private static readonly string WinConfigDir   = AppDomain.CurrentDomain.BaseDirectory + "Config\\";
        private static readonly string LinuxConfigDir = AppDomain.CurrentDomain.BaseDirectory + "Config/";
        public  static readonly string ConfigDir      = IsLinux ? LinuxConfigDir : WinConfigDir;

        //Mongo Auth
        public static bool isLocal { get; set; }
        public static string User { get; set; }
        public static string Pass { get; set; }

    }
    
}