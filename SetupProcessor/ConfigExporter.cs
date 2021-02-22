using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using SetupProcessor.Logger;
using SetupProcessor.Model;
using YamlDotNet.Serialization;

namespace SetupProcessor {
    public static class ConfigExporter {
        private static readonly Action<string> infoLog = (string msg) => AlLite.WriteLine(WriteMode.INFO, msg);

        public static void onExport(ConfigModel model, bool isOverwrite) {
            infoLog.Invoke("Write launcher default config.");
            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "launcher_config.yaml", !isOverwrite, Encoding.UTF8)) {
                Serializer serializer = new Serializer();
                string serializedStr = serializer.Serialize(model);
                sw.Write(serializedStr);
            }
            infoLog.Invoke("Finished Config Generate.");
        }
        
        public static void onTemplateGenerate([Optional] string dbUserName, [Optional] string dbPassWord) {
            infoLog.Invoke("Generating Config...");
            infoLog.Invoke("Get property from app arguments.");
            
            ConfigModel template = new ConfigModel() {
                API_KEY = "Plz Enter Your Youtube API key.",
                DB_ACCESS_USERNAME = dbUserName ?? "USER_NAME",
                DB_ACCESS_PASSWORD = dbPassWord ?? "PASS_WORD",
                IS_LOCAL_MODE      = false,
                ignoreData = new List<ConfigModel.IgnoreData>() {
                    new ConfigModel.IgnoreData() {
                        IgnoreDataName = "Example: FreeChat",
                        IgnoreVideoId  = "***********"
                    }
                }
            };
            
            onExport(template, true);
        } 
    }
}