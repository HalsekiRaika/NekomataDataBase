using System;
using System.Collections.Generic;
using System.Linq;
using SetupLibs;
using SetupLibs.Logger;
using SetupLibs.Model;

namespace ConfigEditor {
    public static class ArgParser {
        private static string apiKey;
        private static string userName;
        private static string passWord;
        private static readonly List<ConfigModel.IgnoreData> ignoreData = new List<ConfigModel.IgnoreData>();
        
        public static void Decomposition(string[] arg) {
            ConfigModel model = ConfigImporter.onDeserialize(false);
            try { 
                for (int i = 0; i < arg.Length; i++) {
                    switch (arg[i]) {
                        case "--clear":
                            ConfigExporter.onTemplateGenerate();
                            break;
                        
                        case "-a":
                        case "--api-key":
                            apiKey = arg[++i];
                            break;
                        
                        case "-u":
                        case "--user":
                            userName = arg[++i];
                            break;
                        
                        case "-p":
                        case "--pass":
                            passWord = arg[++i];
                            break;

                        case "-ia":
                        case "--ignore-add":
                            ignoreData.Add(CheckRequest.onCheck(model, arg[++i]));
                            break;
                        
                        case "-iaa":
                        case "--ignore-add-array":
                            arg[++i].Split(',').ToList().ForEach(videoId => {
                                ignoreData.Add(CheckRequest.onCheck(model, videoId));
                            });
                            break;
                    }
                }
            } catch (IndexOutOfRangeException) {
                AlLite.WriteLine(WriteMode.ERR, "Incorrect Number of Arguments.");
            }
            
            ConfigUpdater.onUpdate(apiKey, userName, passWord, ignoreData);
        }
    }
}