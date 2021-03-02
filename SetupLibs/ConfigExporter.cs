using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using SetupLibs.Logger;
using SetupLibs.Model;
using YamlDotNet.Serialization;

namespace SetupLibs {
    public static class ConfigExporter {
        private static readonly Action<string> infoLog = (string msg) => AlLite.WriteLine(WriteMode.INFO, msg);

        public static void onExport(ConfigModel model, bool isOverwrite) {
            infoLog.Invoke("Write launcher config.");
            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "launcher_config.yaml", !isOverwrite, Encoding.UTF8)) {
                Serializer serializer = new Serializer();
                string serializedStr = serializer.Serialize(model);
                sw.Write(serializedStr);
            }
            infoLog.Invoke("Finished Config Generate.");
        }
        
        public static void onTemplateGenerate([Optional] string dbUserName, [Optional] string dbPassWord) {
            infoLog.Invoke("Generating Config...");
            if (!(dbUserName == null && dbPassWord == null)) {
                infoLog.Invoke("Get property from app arguments.");
            } else {
                infoLog.Invoke("Argument is not set, the default setting will be used.");
            }

            ConfigModel template = new ConfigModel() {
                API_KEY = Settings.ApikeyTemplateMessage,
                DB_ACCESS_USERNAME = dbUserName ?? Settings.UserNameTemplateString,
                DB_ACCESS_PASSWORD = dbPassWord ?? Settings.PassWordTemplateString,
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