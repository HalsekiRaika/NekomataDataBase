using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BaseDataCollector.Extension;
using BaseDataCollector.Logging;
using BaseDataCollector.Structure;
using Log5RLibs.Services;
using MongoDB.Driver;
using Nett;
using RefactorScheme = BaseDataCollector.Structure.RefactorScheme;

namespace BaseDataCollector.Config {
    #region LOADED_COMPONENT
    public static class LoadedComponent {
        private static readonly Dictionary<IMongoDatabase, Dictionary<string, IMongoCollection<RefactorScheme>>> _collection 
            = new Dictionary<IMongoDatabase, Dictionary<string, IMongoCollection<RefactorScheme>>>();
        private static readonly Dictionary<string, string> _channelIdComponent = new Dictionary<string, string>();

        public static void SetCollectionDict(IMongoDatabase database, Dictionary<string, IMongoCollection<RefactorScheme>> targetCollection) {
            _collection.Add(database, targetCollection);
        }

        public static void SetChannelIdComponent(string channelId, string dbName) {
            _channelIdComponent.Add(channelId, dbName);
        }

        public static List<string> GetChannelId() {
            return _channelIdComponent.Select(pair => pair.Key).ToList();
        }

        public static List<string> GetDataBaseName() {
            return _channelIdComponent.Select(pair => pair.Value).ToList();
        }

        public static Dictionary<IMongoDatabase, Dictionary<string, IMongoCollection<RefactorScheme>>> GetAllCollections() {
            return _collection;
        }

        public static Dictionary<string, IMongoCollection<RefactorScheme>> GetCollection() {
            Dictionary<string, IMongoCollection<RefactorScheme>> dictionary = new Dictionary<string, IMongoCollection<RefactorScheme>>();
            foreach (KeyValuePair<IMongoDatabase, Dictionary<string, IMongoCollection<RefactorScheme>>> pair in _collection) {
                foreach (KeyValuePair<string, IMongoCollection<RefactorScheme>> valuePair in pair.Value) {
                    dictionary.Add(valuePair.Key, valuePair.Value);
                }
            }
            return dictionary;
        }
        
        public static string GetDataBaseName(string channelId) {
            return _channelIdComponent[channelId];
        }
    }
    #endregion

    public static class ConfigLoader {
        public static void OnLoadEvent(MongoClient client) {
            AlExtension.ColorizeWriteLine(LogIndex.CONFIG_INFORMATION, 
                new []{"--> ", "FIRST INITIALIZATION."}, 
                new [] {ConsoleColor.Blue, ConsoleColor.Green});

            List<string> configFolders = _ConfigFolderDir();
            Dictionary<string, string[]> configFiles = _ConfigFileDir(configFolders);
            Dictionary<string, Dictionary<string, ConfigScheme>> groupedDict = new Dictionary<string, Dictionary<string, ConfigScheme>>();
            foreach (KeyValuePair<string, string[]> fileComponent in configFiles) {
                Dictionary<string, ConfigScheme> loadedDict  = new Dictionary<string, ConfigScheme>();
                AlExtension.ColorizeWriteLine(LogIndex.CONFIG_INFORMATION, 
                    new []{ "Parse Group [ ", $"{fileComponent.Key.Substring(GeneralSettings.ConfigDir.Length)}", " ]" }, 
                    new []{ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Green});
                foreach (string file in fileComponent.Value) {
                    using (StreamReader reader = new StreamReader(file)) {
                        string raw = reader.ReadToEnd();
                        ConfigScheme clazzParse = Toml.ReadString<ConfigScheme>(raw);
                        AlExtension.ColorizeWriteLine(LogIndex.CONFIG_INFORMATION, 
                            new []{ $"{clazzParse.Profile.Name + ".toml", 32}", " => Parsed!" },
                            new [] {ConsoleColor.Magenta, ConsoleColor.Green});
                        loadedDict.Add(clazzParse.Profile.DBName, clazzParse);
                    }
                }
                groupedDict.Add(fileComponent.Key.Substring(GeneralSettings.ConfigDir.Length), loadedDict);
            }
            AlConsole.WriteLine(LogIndex.CONFIG_INFORMATION, $"Loaded All Config Operational!");
            AlExtension.ColorizeWriteLine(LogIndex.CONFIG_INFORMATION, 
                new []{ "--> ", "SECOND INITIALIZATION." }, 
                new [] {ConsoleColor.Blue, ConsoleColor.Green});
            // AlConsole.WriteLine(CONFIG_INFORMATION, "-> Second Initialization.");
            GenerateDataBaseProperty(client, groupedDict);
            AlConsole.WriteLine(LogIndex.CONFIG_INFORMATION, $"Generated DataBase Property from Loaded Config.");
        }

        // Dictionary<MemberName(string), Dictionary<DataBaseName(string), DBProperty(IMongoCollection<RefactorScheme>)>>
        private static void GenerateDataBaseProperty(MongoClient client, Dictionary<string, Dictionary<string, ConfigScheme>> configComponent) {
            foreach (KeyValuePair<string, Dictionary<string, ConfigScheme>> groupedCorp in configComponent) {
                Dictionary<string, IMongoCollection<RefactorScheme>> collectionsBuf = new Dictionary<string, IMongoCollection<RefactorScheme>>();
                IMongoDatabase targetDataBase = client.GetDatabase(groupedCorp.Key);
                AlExtension.ColorizeWriteLine(LogIndex.CONFIG_INFORMATION, 
                    new []{ " => Generate DataBase Property" }, 
                    new [] {ConsoleColor.Cyan});
                AlExtension.ColorizeWriteLine(LogIndex.CONFIG_INFORMATION, 
                    new []{ " ┏ ", "DataBase[ ", $"{groupedCorp.Key}", " ]" }, 
                    new [] {ConsoleColor.DarkGray, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Green});
                foreach (KeyValuePair<string, ConfigScheme> configDict in groupedCorp.Value) {
                    IMongoCollection<RefactorScheme> generatedCollection =
                        targetDataBase.GetCollection<RefactorScheme>(configDict.Key);
                    AlExtension.ColorizeWrite(LogIndex.CONFIG_INFORMATION, 
                        new []{ " ┣ ", "Collection[ ", $"{configDict.Key, -26}", " ]" },
                        new [] {ConsoleColor.DarkGray, ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.Magenta});
                    
                    collectionsBuf.Add(configDict.Key, generatedCollection);
                    LoadedComponent.SetChannelIdComponent(configDict.Value.ChannelData[0].Details[0].ID.ToString(), configDict.Key);
                    AlExtension.ColorizeWriteLine(LogIndex.CONFIG_INFORMATION, 
                        new []{ $" / ", "ChannelId[ ", $"{configDict.Value.ChannelData[0].Details[0].ID, -16}", " ]" },
                        new [] {ConsoleColor.DarkGray, ConsoleColor.DarkMagenta, ConsoleColor.DarkCyan, ConsoleColor.DarkMagenta}, false);
                }
                LoadedComponent.SetCollectionDict(targetDataBase, collectionsBuf);
                AlExtension.ColorizeWriteLine(LogIndex.CONFIG_INFORMATION, new []{ " ┗ ", "[ EOT ]" }, 
                    new [] {ConsoleColor.DarkGray, ConsoleColor.Green});
            }
        }

        private static void _DetectSubChannelProperty() {
            
        }

        private static List<string> _ConfigFolderDir() {
            DirectoryInfo dInfo = new DirectoryInfo(GeneralSettings.ConfigDir);
            if (!dInfo.Exists) {
                string[] errMsg = new [] {
                    "Configディレクトリが存在しません。",
                    $"[ {GeneralSettings.ConfigDir} ]に以下URLのConfigファイルをセットしてください。",
                    "NekomataLibrary: https://github.com/ReiRokusanami0010/NekomataLibrary"
                };
                AlExtension.ArrayWrite(LogIndex.CONFIG_EXCEPTION, errMsg);
                Environment.Exit(-1);
            }
            List<string> detectDirNames = Directory.GetDirectories(GeneralSettings.ConfigDir).ToList<string>();
            detectDirNames.Remove($"{GeneralSettings.ConfigDir}.git");
            foreach (string detectName in detectDirNames) {
                AlExtension.ColorizeWriteLine(LogIndex.CONFIG_INFORMATION, 
                    new []{ "Detect Corp Folder -> ", $"[ ${detectName, -18} ]" },
                    new [] {ConsoleColor.Green, ConsoleColor.Blue});
                // AlConsole.WriteLine(CONFIG_INFORMATION, $"Detect Corp Folder -> [ {detectName, -18} ]");
            }

            return detectDirNames;
        }

        // Dictionary<FolderName(string), FileName(string[])>
        private static Dictionary<string, string[]> _ConfigFileDir(List<string> targetConfig) {
            Dictionary<string, string[]> configInfo = new Dictionary<string, string[]>();
            foreach (string folder in targetConfig) {
                configInfo.Add(folder, Directory.GetFiles($"{folder}" + (GeneralSettings.IsLinux ? "/" : "\\"), "*.toml"));
            }
            
            return configInfo;
        }
    }
}