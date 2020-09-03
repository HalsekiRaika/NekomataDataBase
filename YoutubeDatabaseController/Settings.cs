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
        public static readonly string ConfigDir = AppDomain.CurrentDomain.BaseDirectory + "Config\\";
        
        //Mongo Auth
        public static bool isLocal { get; set; }
        public static string User { get; set; }
        public static string Pass { get; set; }

    }
    
}