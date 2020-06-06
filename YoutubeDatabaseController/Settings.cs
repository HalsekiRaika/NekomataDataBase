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
        public static readonly string HoloLive = "Hololive";
        public static readonly string NijiSanji = "Nijisanji";
        public static readonly string AniMare = "Animare";

    }
    
}