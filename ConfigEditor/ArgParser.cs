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
                if (arg.Length < 1) { WriteGuide(); return; }
                for (int i = 0; i < arg.Length; i++) {
                    switch (arg[i]) {
                        case "-c":
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
                
                ConfigUpdater.onUpdate(apiKey, userName, passWord, ignoreData);
                
            } catch (IndexOutOfRangeException) {
                AlLite.WriteLine(WriteMode.ERR, "Incorrect Number of Arguments.\n");
                WriteGuide();
            }
        }

        private static void WriteGuide() {
            Console.WriteLine(
                "This app is an editor for configuring the Launcher.\n\n"
                + "Args (Reduction/Regular)  Second Arg(s)          : Description\n"
                + "--------------------------------------------------------------------------------------\n"
                + "-c   / --clear                                   : Generate(overwrite) default config.\n" 
                + "-a   / --api-key          <API_KEY>              : Set YoutubeDataApi Apikey.\n" 
                + "-u   / --user             <DB_USERNAME>          : Set MongoDB UserName.\n" 
                + "-p   / --pass             <DB_PASSWORD>          : Set MongoDB PassWord.\n"
                + "-ia  / --ignore-add       <TARGET_VIDEO_ID>      : Set Ignore Collect Target.\n"
                + "-iaa / --ignore-add-array <TARGET_VIDEO_ID, ...> : Set Ignore Collect Targets.\n\n"
            );
        }
    }
}