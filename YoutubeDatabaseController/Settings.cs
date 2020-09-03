using System;

namespace YoutubeDatabaseController {
    public static class Settings {
        public static string[] Startup = new string[] {
            "------------------ RUN START ------------------",
            $"{DateTime.Now:F}",
            "Tool was Created By ReiRokusanami",
            "-----------------------------------------------"
        };

        // Production Names
        public static readonly string Hololive = "Hololive";
        public static readonly string Nijisanji = "Nijisanji";
        public static readonly string AniMare = "Animare";
        
        // Server
        public static readonly string NekomataAws   = "ec2-3-91-37-39.compute-1.amazonaws.com";
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