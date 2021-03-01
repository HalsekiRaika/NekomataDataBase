using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BaseDataCollector;
using BaseDataCollector.Extension;
using Log5RLibs.Services;
using MongoDB.Driver;
using Nett;
using YoutubeDatabaseController.Scheme;
using static YtDataCollector.Scheme.LogScheme.DefaultScheme;

namespace YtDataCollector {
    #region LOADED_COMPONENT
    public static class LoadedComponent {
        private static Dictionary<string, IMongoCollection<RefactorScheme>> _collections = new Dictionary<string, IMongoCollection<RefactorScheme>>();
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
            List<string> channelId = new List<string>();
            foreach (KeyValuePair<string, string> pair in _channelIdComponent) {
                channelId.Add(pair.Key);
            }
            return channelId;
        }

        public static List<string> GetDataBaseName() {
            List<string> databaseName = new List<string>();
            foreach (KeyValuePair<string, string> pair in _channelIdComponent) {
                databaseName.Add(pair.Value);
            }
            return databaseName;
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
            AlExtension.ColorizeWriteLine(CONFIG_INFORMATION, "-->^ FIRST INITIALIZATION.", new [] {ConsoleColor.Blue, ConsoleColor.Green});
            List<string> configFolders = _ConfigFolderDir();
            Dictionary<string, string[]> configFiles = _ConfigFileDir(configFolders);
            Dictionary<string, Dictionary<string, ConfigScheme>> groupedDict = new Dictionary<string, Dictionary<string, ConfigScheme>>();
            foreach (KeyValuePair<string, string[]> fileComponent in configFiles) {
                Dictionary<string, ConfigScheme> loadedDict  = new Dictionary<string, ConfigScheme>();
                AlExtension.ColorizeWriteLine(CONFIG_INFORMATION, $"Parse Group [ ^{fileComponent.Key.Substring(Settings.ConfigDir.Length)}^ ]", 
                    new []{ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Green});
                // AlConsole.WriteLine(CONFIG_INFORMATION, $"Parse Group [ {fileComponent.Key.Substring(Settings.ConfigDir.Length)} ]");
                foreach (string file in fileComponent.Value) {
                    using (StreamReader reader = new StreamReader(file)) {
                        string raw = reader.ReadToEnd();
                        ConfigScheme clazzParse = Toml.ReadString<ConfigScheme>(raw);
                        AlExtension.ColorizeWriteLine(CONFIG_INFORMATION, $"{clazzParse.Profile.Name + ".toml", 32}^ => Parsed!",
                            new [] {ConsoleColor.Magenta, ConsoleColor.Green});
                        // AlConsole.WriteLine(CONFIG_INFORMATION, $"{clazzParse.Profile.Name + ".toml", 32} => Parsed!");
                        loadedDict.Add(clazzParse.Profile.DBName, clazzParse);
                    }
                }
                groupedDict.Add(fileComponent.Key.Substring(Settings.ConfigDir.Length), loadedDict);
            }
            AlConsole.WriteLine(CONFIG_INFORMATION, $"Loaded All Config Operational!");
            AlExtension.ColorizeWriteLine(CONFIG_INFORMATION, "-->^ SECOND INITIALIZATION.", new [] {ConsoleColor.Blue, ConsoleColor.Green});
            // AlConsole.WriteLine(CONFIG_INFORMATION, "-> Second Initialization.");
            GenerateDataBaseProperty(client, groupedDict);
            AlConsole.WriteLine(CONFIG_INFORMATION, $"Generated DataBase Property from Loaded Config.");
        }

        // Dictionary<MemberName(string), Dictionary<DataBaseName(string), DBProperty(IMongoCollection<RefactorScheme>)>>
        private static void GenerateDataBaseProperty(MongoClient client, Dictionary<string, Dictionary<string, ConfigScheme>> configComponent) {
            foreach (KeyValuePair<string, Dictionary<string, ConfigScheme>> groupedCorp in configComponent) {
                Dictionary<string, IMongoCollection<RefactorScheme>> collectionsBuf = new Dictionary<string, IMongoCollection<RefactorScheme>>();
                IMongoDatabase targetDataBase = client.GetDatabase(groupedCorp.Key);
                AlExtension.ColorizeWriteLine(CONFIG_INFORMATION, " => Generate DataBase Property", 
                    new [] {ConsoleColor.Cyan, ConsoleColor.Green});
                // AlConsole.WriteLine(CONFIG_INFORMATION, " => Generate DataBase Property");
                AlExtension.ColorizeWriteLine(CONFIG_INFORMATION, $" ┏ ^DataBase[ ^{groupedCorp.Key} ^]", 
                    new [] {ConsoleColor.DarkGray, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Green});
                // AlConsole.WriteLine(CONFIG_INFORMATION, $" ┏ DataBase[ {groupedCorp.Key} ]");
                foreach (KeyValuePair<string, ConfigScheme> configDict in groupedCorp.Value) {
                    IMongoCollection<RefactorScheme> generatedCollection =
                        targetDataBase.GetCollection<RefactorScheme>(configDict.Key);
                    AlExtension.ColorizeWrite(CONFIG_INFORMATION, " ┣ ^Collection[ ^" + $"{configDict.Key, -26}" + " ^]",
                        new [] {ConsoleColor.DarkGray, ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.Magenta});
                    // AlConsole.WriteLine(CONFIG_INFORMATION, $" ┣ Collection[ " + $"{configDict.Key, -26}" + " ]");
                    collectionsBuf.Add(configDict.Key, generatedCollection);
                    LoadedComponent.SetChannelIdComponent(configDict.Value.ChannelData[0].Details[0].ID.ToString(), configDict.Key);
                    AlExtension.ColorizeWriteLine(CONFIG_INFORMATION, $" / ^ChannelId[ " + $"^{configDict.Value.ChannelData[0].Details[0].ID.ToString(), -16}" + " ^]",
                        new [] {ConsoleColor.DarkGray, ConsoleColor.DarkMagenta, ConsoleColor.DarkCyan, ConsoleColor.DarkMagenta}, false);
                    // AlConsole.WriteLine(CONFIG_INFORMATION, $" ┃  ┗ ChannelId[ " + $"{configDict.Value.ChannelData[0].Details[0].ID.ToString(), -16}" + " ]");
                }
                LoadedComponent.SetCollectionDict(targetDataBase, collectionsBuf);
                AlExtension.ColorizeWriteLine(CONFIG_INFORMATION, $" ┗ ^[ EOT ]", new [] {ConsoleColor.DarkGray, ConsoleColor.Green});
                // AlConsole.WriteLine(CONFIG_INFORMATION, $" ┗ [ EOT ]");
            }
        }

        private static void _DetectSubChannelProperty() {
            
        }

        private static List<string> _ConfigFolderDir() {
            DirectoryInfo dInfo = new DirectoryInfo(Settings.ConfigDir);
            if (!dInfo.Exists) {
                string[] errMsg = new string[] {
                    "Configディレクトリが存在しません。",
                    $"[ {Settings.ConfigDir} ]に以下URLのConfigファイルをセットしてください。",
                    "NekomataLibrary: https://github.com/ReiRokusanami0010/NekomataLibrary"
                };
                AlExtension.ArrayWrite(CONFIG_EXCEPTION, errMsg);
                Environment.Exit(-1);
            }
            List<string> detectDirNames = Directory.GetDirectories(Settings.ConfigDir).ToList<string>();
            detectDirNames.Remove($"{Settings.ConfigDir}.git");
            foreach (string detectName in detectDirNames) {
                AlExtension.ColorizeWriteLine(CONFIG_INFORMATION, $"Detect Corp Folder -> ^[ {detectName, -18} ]",
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