using System;
using System.Collections.Generic;
using System.IO;
using Log5RLibs.Services;
using Nett;
using YoutubeDatabaseController.Scheme;
using static YoutubeDatabaseController.Scheme.LogScheme.DefaultScheme;

namespace YoutubeDatabaseController {
    public static class ConfigLoader {
        public static Dictionary<string, ConfigScheme> OnLoadEvent() {
            string[] configHololiveFiles = _ConfigFileDir(Settings.Hololive);
            Dictionary<string, ConfigScheme> loadedDict = new Dictionary<string, ConfigScheme>();
            foreach (string file in configHololiveFiles) {
                using (StreamReader reader = new StreamReader(file)) {
                    string raw = reader.ReadToEnd();
                    ConfigScheme clazzParse = Toml.ReadString<ConfigScheme>(raw);
                    AlConsole.WriteLine(CONFIG_INFORMATION, $"{clazzParse.Profile.Name}.toml => Parsed!");
                    loadedDict.Add(clazzParse.Profile.DBName, clazzParse);
                }
            }
            AlConsole.WriteLine(CONFIG_INFORMATION, $"All Config Operational!");
            
            return loadedDict;
        }

        private static string[] _ConfigFileDir(string targetConfig) {
            DirectoryInfo dInfo = new DirectoryInfo(Settings.ConfigDir);
            if (!dInfo.Exists) {
                AlConsole.WriteLine(CONFIG_EXCEPTION, "Configディレクトリが存在しません。");
                AlConsole.WriteLine(CONFIG_EXCEPTION, $"[ {Settings.ConfigDir} ]に以下URLのConfigファイルをセットしてください。");
                AlConsole.WriteLine(CONFIG_EXCEPTION, $"NekomataLibrary: https://github.com/ReiRokusanami0010/NekomataLibrary");
                Environment.Exit(-1);
            }
            return Directory.GetFiles(Settings.ConfigDir + $"{targetConfig}\\", "*.toml");
        }
    }
}