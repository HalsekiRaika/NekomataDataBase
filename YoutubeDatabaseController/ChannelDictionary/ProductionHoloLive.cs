using System.Collections.Generic;

namespace YoutubeDatabaseController.ChannelDictionary {
    public static class ProductionHoloLive {
        
        // Channel Names
        public const string NatsuiroMatsuri  = "MatsuriChannel";
        public const string OozoraSubaru     = "SubaruChannel";
        public const string Akirosenthal     = "AkirozeChannel";
        public const string AkaiHaato        = "HaatoChannel";
        public const string NakiriAyame      = "NakiriChannel";
        public const string InugamiKorone    = "KoroneChannel";
        public const string ShiranuiFlare    = "FlareChannel";
        public const string TokoyamiTowa     = "TowaChannel";
        public const string UsadaPekora      = "PekoraChannel";
        public const string MinatoAqua       = "AquaChannel";
        public const string KiryuCoco        = "CocoChannel";
        public const string HoshimachiSuisei = "SuiseiChannel";
        public const string NekomataOkayu    = "OkayuChannel";
        public const string TsunomakiWatame  = "WatameChannel";
        public const string UruhaRushia      = "RushiaChnnel";
        public const string ShiroganeNoel    = "ShiroganeNoel";
        public const string YozoraMel        = "MelChannel";
        public const string Roboco           = "RobocoChannel";
        public const string TokinoSora       = "SoraChannel";
        public const string ShirakamiFubuki  = "FubukiChannel";
        public const string OokamiMio        = "MioChannel";
        public const string MurasakiShion    = "ShionChannel";
        public const string AmaneKanata      = "KanataChannel";
        public const string YuzukiChoco      = "ChocoChannel";
        public const string YuzukiChocoSub   = "ChocoSubChannel";
        public const string HoushouMarine    = "MarineChannel";
        public const string HimemoriLuna     = "LunaChannel";
        public const string SakuraMiko       = "MikoChannel";
        public const string AZKi             = "AZKiChannel";

        // Channel Id Dictionary
        private static readonly Dictionary<string, string> IdDict = new Dictionary<string, string>() {
            {"UCQ0UDLQCjY0rmuxCDE38FGg",  NatsuiroMatsuri},
            {"UCvzGlP9oQwU--Y0r9id_jnA",     OozoraSubaru},
            {"UCFTLzh12_nrtzqBPsTCqenA",     Akirosenthal},
            {"UC1CfXB_kRs3C-zaeTG3oGyg",        AkaiHaato},
            {"UC7fk0CB07ly8oSl0aqKkqFg",      NakiriAyame},
            {"UChAnqc_AY5_I3Px5dig3X1Q",    InugamiKorone},
            {"UCvInZx9h3jC2JzsIzoOebWg",    ShiranuiFlare},
            {"UC1uv2Oq6kNxgATlCiez59hw",     TokoyamiTowa},
            {"UC1DCedRgGHBdm81E1llLhOQ",      UsadaPekora},
            {"UC1opHUrw8rvnsadT-iGp7Cg",       MinatoAqua},
            {"UCS9uQI-jC3DE0L4IpXyvr6w",        KiryuCoco},
            {"UC5CwaMl1eIgY8h02uZw7u8A", HoshimachiSuisei},
            {"UCvaTdHTWBGv3MKj3KVqJVCw",    NekomataOkayu},
            {"UCqm3BQLlJfvkTsX_hvm0UmA",  TsunomakiWatame},
            {"UCl_gCybOJRIgOXw6Qb4qJzQ",      UruhaRushia},
            {"UCdyqAaZDKHXg4Ahi7VENThQ",    ShiroganeNoel},
            {"UCD8HOxPs4Xvsm8H0ZxXGiBw",        YozoraMel},
            {"UCDqI2jOz0weumE8s7paEk6g",           Roboco},
            {"UCp6993wxpyDPHUpavwDFqgg",       TokinoSora},
            {"UCdn5BQ06XqgXoAxIhbqw5Rg",  ShirakamiFubuki},
            {"UCp-5t9SrOQwXMU7iIjQfARg",        OokamiMio},
            {"UCXTpFs_3PqI41qX2d9tL2Rw",    MurasakiShion},
            {"UCZlDXzGoo7d44bwdNObFacg",      AmaneKanata},
            {"UC1suqwovbL1kzsoaZgFZLKg",      YuzukiChoco},
            {"UCp3tgHXw_HI0QMk1K8qh3gQ",   YuzukiChocoSub},
            {"UCCzUftO8KOVkV4wQG1vkUvg",    HoushouMarine},
            {"UCa9Y57gfeY0Zro_noHRVrnw",     HimemoriLuna},
            {"UC-hM6YJuNYVAmUWxeIr9FeA",       SakuraMiko},
            {"UC0TXe_LYZ4scaW2XMyi5_kw",             AZKi},
        };

        public static List<string> GetAllValue() {
            List<string> buf = new List<string>();
            foreach (KeyValuePair<string, string> item in IdDict) {
                buf.Add(item.Value);
            }
            return buf;
        }
        
        public static List<string> GetAllKey() {
            List<string> buf = new List<string>();
            foreach (KeyValuePair<string, string> item in IdDict) {
                buf.Add(item.Key);
            }
            return buf;
        }
        
        public static string GetChannelName(string channelId) {
            return IdDict[channelId];
        }
    }
}