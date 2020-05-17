using System.Collections.Generic;

namespace YoutubeDatabaseController.Util {
    public class ChannelIDList {
        private static readonly List<string> ChannelID = new List<string>() {
            // ---------  HoloLive  ----------//
            "UCQ0UDLQCjY0rmuxCDE38FGg", //Matsuri Channel 夏色まつり
            "UCvzGlP9oQwU--Y0r9id_jnA", //Subaru Ch. 大空スバル
            "UCFTLzh12_nrtzqBPsTCqenA", //アキロゼCh。Vtuber/ホロライブ所属
            "UC1CfXB_kRs3C-zaeTG3oGyg", //Haato Channel 赤井はあと
            "UC7fk0CB07ly8oSl0aqKkqFg", //Nakiri Ayame Ch. 百鬼あやめ
            "UChAnqc_AY5_I3Px5dig3X1Q", //Korone Ch. 戌神ころね
            "UCvInZx9h3jC2JzsIzoOebWg", //Flare Ch. 不知火フレア
            "UC1uv2Oq6kNxgATlCiez59hw", //Towa Ch. 常闇トワ
            "UC1DCedRgGHBdm81E1llLhOQ", //Pekora Ch. 兎田ぺこら
            "UC1opHUrw8rvnsadT-iGp7Cg", //Aqua Ch. 湊あくあ
            "UCS9uQI-jC3DE0L4IpXyvr6w", //Coco Ch. 桐生ココ
            "UC5CwaMl1eIgY8h02uZw7u8A", //Suisei Channel
            "UCvaTdHTWBGv3MKj3KVqJVCw", //Okayu Ch. 猫又おかゆ
            "UCqm3BQLlJfvkTsX_hvm0UmA", //Watame Ch. 角巻わため
            "UCl_gCybOJRIgOXw6Qb4qJzQ", //Rushia Ch. 潤羽るしあ
            "UCdyqAaZDKHXg4Ahi7VENThQ", //Noel Ch. 白銀ノエル
            "UCD8HOxPs4Xvsm8H0ZxXGiBw", //Mel Channel 夜空メルチャンネル
            "UCDqI2jOz0weumE8s7paEk6g", //Roboco Ch. - ロボ子
            "UCp6993wxpyDPHUpavwDFqgg", //SoraCh. ときのそらチャンネル
            "UCdn5BQ06XqgXoAxIhbqw5Rg", //フブキCh。白上フブキ
            "UC0TXe_LYZ4scaW2XMyi5_kw", //AZKi Channel
            "UCp-5t9SrOQwXMU7iIjQfARg", //Mio Channel 大神ミオ
            "UCXTpFs_3PqI41qX2d9tL2Rw", //Shion Ch. 紫咲シオン
            "UCZlDXzGoo7d44bwdNObFacg", //Kanata Ch. 天音かなた
            "UC1suqwovbL1kzsoaZgFZLKg", //Choco Ch. 癒月ちょこ
            "UCCzUftO8KOVkV4wQG1vkUvg", //Marine Ch. 宝鐘マリン
            "UCa9Y57gfeY0Zro_noHRVrnw", //Luna Ch. 姫森ルーナ
            "UC-hM6YJuNYVAmUWxeIr9FeA", //Miko Ch. さくらみこ
            // ------------------------------ //
        };

        public static List<string> GetChannelId() {
            return ChannelID;
        }
    }
}