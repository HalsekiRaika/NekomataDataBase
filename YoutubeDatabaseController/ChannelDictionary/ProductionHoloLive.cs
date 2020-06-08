using System.Collections.Generic;

namespace YoutubeDatabaseController.ChannelDictionary {
    public class ProductionHoloLive {
        
        // Channel Names
        public static readonly string NatsuiroMatsuri  = "MatsuriChannel";
        public static readonly string OozoraSubaru     = "SubaruChannel";
        public static readonly string Akirosenthal     = "AkirozeChannel";
        public static readonly string AkaiHaato        = "HaatoChannel";
        public static readonly string NakiriAyame      = "NakiriChannel";
        public static readonly string InugamiKorone    = "KoroneChannel";
        public static readonly string ShiranuiFlare    = "FlareChannel";
        public static readonly string TokoyamiTowa     = "TowaChannel";
        public static readonly string UsadaPekora      = "PekoraChannel";
        public static readonly string MinatoAqua       = "AquaChannel";
        public static readonly string KiryuuCoco       = "CocoChannel";
        public static readonly string HoshimachiSuisei = "SuiseiChannel";
        public static readonly string NekomataOkayu    = "OkayuChannel";
        public static readonly string TsunomakiWatame  = "WatameChannel";
        public static readonly string UruhaRushia      = "UruhaRushia";
        public static readonly string ShiroganeNoel    = "ShiroganeNoel";
        public static readonly string YozoraMel        = "MelChannel";
        public static readonly string Roboco           = "RobocoChannel";
        public static readonly string TokinoSora       = "SoraChannel";
        public static readonly string ShirakamiFubuki   = "FubukiChannel";
        public static readonly string OokamiMio        = "MioChannel";
        public static readonly string MurasakiShion    = "ShionChannel";
        public static readonly string AmaneKanata      = "KanataChannel";
        public static readonly string YuzukiChoco      = "ChocoChannel";
        public static readonly string HoushouMarine    = "MarineChannel";
        public static readonly string HimemoriLuna     = "LunaChannel";
        public static readonly string SakuraMiko       = "MikoChannel";
        public static readonly string AZKi             = "AZKiChannel";

        // Channel Id Dictionary
        private static readonly Dictionary<string, string> IdDict = new Dictionary<string, string>() {
            {"UCQ0UDLQCjY0rmuxCDE38FGg", NatsuiroMatsuri},
            {"UCvzGlP9oQwU--Y0r9id_jnA",    OozoraSubaru},
            {"UCFTLzh12_nrtzqBPsTCqenA",    Akirosenthal},
            {"UC1CfXB_kRs3C-zaeTG3oGyg",       AkaiHaato},
            {"UC7fk0CB07ly8oSl0aqKkqFg",     NakiriAyame},
            {"UChAnqc_AY5_I3Px5dig3X1Q",   InugamiKorone},
            {"UCvInZx9h3jC2JzsIzoOebWg",   ShiranuiFlare},
            {"UC1uv2Oq6kNxgATlCiez59hw",    TokoyamiTowa},
            {"UC1DCedRgGHBdm81E1llLhOQ",     UsadaPekora},
            {"UC1opHUrw8rvnsadT-iGp7Cg",      MinatoAqua},
            {"UCS9uQI-jC3DE0L4IpXyvr6w",      KiryuuCoco},
            {"UC5CwaMl1eIgY8h02uZw7u8A",HoshimachiSuisei},
            {"UCvaTdHTWBGv3MKj3KVqJVCw",   NekomataOkayu},
            {"UCqm3BQLlJfvkTsX_hvm0UmA", TsunomakiWatame},
            {"UCl_gCybOJRIgOXw6Qb4qJzQ",     UruhaRushia},
            {"UCdyqAaZDKHXg4Ahi7VENThQ",   ShiroganeNoel},
            {"UCD8HOxPs4Xvsm8H0ZxXGiBw",       YozoraMel},
            {"UCDqI2jOz0weumE8s7paEk6g",          Roboco},
            {"UCp6993wxpyDPHUpavwDFqgg",      TokinoSora},
            {"UCdn5BQ06XqgXoAxIhbqw5Rg", ShirakamiFubuki},
            {"UCp-5t9SrOQwXMU7iIjQfARg",       OokamiMio},
            {"UCXTpFs_3PqI41qX2d9tL2Rw",   MurasakiShion},
            {"UCZlDXzGoo7d44bwdNObFacg",     AmaneKanata},
            {"UC1suqwovbL1kzsoaZgFZLKg",     YuzukiChoco},
            {"UCCzUftO8KOVkV4wQG1vkUvg",   HoushouMarine},
            {"UCa9Y57gfeY0Zro_noHRVrnw",    HimemoriLuna},
            {"UC-hM6YJuNYVAmUWxeIr9FeA",      SakuraMiko},
            {"UC0TXe_LYZ4scaW2XMyi5_kw",            AZKi},
        };

        public static List<string> GetAllValue() {
            List<string> buf = new List<string>();
            foreach (KeyValuePair<string, string> item in IdDict) {
                buf.Add(item.Value);
            }
            return buf;
        }
        
        public static string GetChannelName(string channelId) {
            return IdDict[channelId];
        }
    }
}