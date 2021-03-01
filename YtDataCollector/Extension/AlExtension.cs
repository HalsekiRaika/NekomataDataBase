using System;
using BaseDataCollector.Structure.LogScheme;
using Log5RLibs.Services;
using YoutubeDatabaseController.Scheme;

namespace YtDataCollector.Extension {
    public static class AlExtension {
        private static readonly object LockObj = new object();
        public static void ArrayWrite(AlCConfigScheme preset, string[] array) {
            foreach (string writeObj in array) {
                lock (LockObj) {
                    AlConsole.WriteLine(preset, writeObj);
                }
            }
        }

        public static void ColorizeWriteLine(AlCConfigScheme preset, string message, ConsoleColor[] colors, bool isAlConsole = true) {
            ColorizeWrite(preset, message, colors, isAlConsole);
            Console.WriteLine();
        }
        
        public static void ColorizeWrite(AlCConfigScheme preset, string message, ConsoleColor[] colors, bool isAlConsole = true) {
            if (isAlConsole) { AlConsole.Write(preset, ""); }
            string[] disParsedMessage = message.Split("^");
            for (int i = 0; i < disParsedMessage.Length; i++) {
                Console.ForegroundColor = colors[i];
                Console.Write(disParsedMessage[i]);
            }
            Console.ResetColor();
        }

        #region EXPAND_WRITE

        public static void ExpandWrite(RefactorScheme schemes) {
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, "以下のデータをコレクションに格納します。");
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $"生放送予定枠：");
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" => UUID          : {schemes._id}");
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" => タイトル　　  ：{schemes.Title}");
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" => チャンネル名  ：{schemes.ChannelName}");
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" => チャンネルID　：{schemes.ChannelId}");
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" ");
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" ================================================ ");
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" ");
        }

        #endregion
    }
}