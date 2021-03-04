using System;
using System.IO;
using SetupLibs.Logger;
using SetupLibs.Model;
using YamlDotNet.Serialization;

namespace SetupLibs {
    public static class ConfigImporter {
        private static readonly Action<string> onLogAction = msg => AlLite.WriteLine(WriteMode.INFO, msg);
        
        public static ConfigModel onDeserialize(bool isDisplay = true, bool isSafeMode = false) {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "launcher_config.yaml")) {
                AlLite.WriteLine(WriteMode.WARN, "Config File is not found.");
                if (isSafeMode) { throw new FileLoadException("You forgot the Config file, you have to generate it with Editor."); }
                return null;
            }
            onLogAction.Invoke("Deserialize launcher config.");
            using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "launcher_config.yaml")) {
                Deserializer deserializer = new Deserializer();
                onLogAction.Invoke("Read the config file...");
                string readString = sr.ReadToEnd();
                onLogAction.Invoke("Return deserialize object.");
                ConfigModel model = deserializer.Deserialize<ConfigModel>(readString);
                if (isDisplay) {
                    onLogAction.Invoke("+----------+----------+----------+----------+");
                    onLogAction.Invoke(" => " + "API_KEY: ".PadLeft(16) + $"{model.API_KEY.Substring(0, 4)} [SECRET]");
                    onLogAction.Invoke(" => " + "DB_ACCESS_USER: " + $"{model.DB_ACCESS_USERNAME}");
                    onLogAction.Invoke(" => " + "DB_ACCESS_PASS: " + $"{Encrypter.onEncrypt(model.DB_ACCESS_PASSWORD)}");
                    onLogAction.Invoke(" => " + "IGNORE_CASE: ".PadLeft(16) + $"{model.ignoreData.Count}");
                    onLogAction.Invoke("+----------+----------+----------+----------+");
                }
                return model;
            }
        }
    }
}