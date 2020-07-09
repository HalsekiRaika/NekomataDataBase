using System;
using System.Collections.Generic;

namespace YoutubeDatabaseController {
    public class Settings {
        public static String StartupMessage = 
            "------------------ RUN START ------------------\n" + 
           $"{DateTime.Now:F}\n" +
            "Target YoutubeChannel Dump\n" +
            "Tool was Created By ReiRokusanami\n" +
            "-----------------------------------------------\n";

        // Production Names
        public static readonly string Hololive = "Hololive";
        public static readonly string Nijisanji = "Nijisanji";
        public static readonly string AniMare = "Animare";
        
        //Mongo Auth
        public static string User { get; set; }
        public static string Psss { get; set; }

    }
    
}