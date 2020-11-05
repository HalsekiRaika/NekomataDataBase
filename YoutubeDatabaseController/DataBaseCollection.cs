using System;
using System.Collections.Generic;
using Log5RLibs.Services;
using MongoDB.Driver;
using YoutubeDatabaseController.Extension;
using YoutubeDatabaseController.Scheme;
using YoutubeDatabaseController.Scheme.LogScheme;

namespace YoutubeDatabaseController {
    public static class DataBaseCollection {
        public static void Insert(MongoClient client, List<RefactorScheme> schemeList) {
            // Start Initialization
            AlConsole.WriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $"初期化を開始します。");

            foreach (KeyValuePair<IMongoDatabase, Dictionary<string, IMongoCollection<RefactorScheme>>> collection in LoadedComponent.GetAllCollections()) {
                AlExtension.ColorizeWriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $" ┏ ^DataBase  ^: ^{collection.Key.DatabaseNamespace.DatabaseName}",
                    new [] {ConsoleColor.DarkGray, ConsoleColor.Green, ConsoleColor.DarkGray, ConsoleColor.Magenta});
                // AlConsole.WriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $" ┏ DataBase  : {collection.Key.DatabaseNamespace.DatabaseName}");
                foreach (KeyValuePair<string, IMongoCollection<RefactorScheme>> collectionDict in collection.Value) {
                    Initialization(collection.Key, collectionDict.Key);
                }
            }

            AlExtension.ColorizeWriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $" ┗ ^全ての初期化が終了しました。", 
                new [] {ConsoleColor.DarkGray, ConsoleColor.Green});
            
            // AlConsole.WriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $" ┗ 全ての初期化が終了しました。");
            AlConsole.WriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $"初期化しました。");
            
            // Start Insert Data
            
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, "回収したデータをコレクションに格納します。");
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" ");
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" ================================================ ");
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" ");
            
            schemeList.ForEach(schemes => {
                string searchedObject = LoadedComponent.GetDataBaseName(schemes.ChannelId); //ProductionHoloLive.GetChannelName(schemes.ChannelId);
                AlExtension.ExpandWrite(schemes);
                //collections[searchedObject].InsertOne(schemes);
                LoadedComponent.GetCollection()[searchedObject].InsertOne(schemes);
                
            });
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_COMP, "全ての挿入が成功しました。");
        }

        private static void Initialization(IMongoDatabase db, string targetCollection) {
            AlExtension.ColorizeWrite(DefaultScheme.DB_INITIALIZE_SCHEME, $" ┣ ^Init Collection: ^{targetCollection, -20} ", 
                new [] {ConsoleColor.DarkGray, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Green});
            // AlConsole.WriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $" ┣ Init Collection: {targetCollection}");
            db.DropCollection(targetCollection);
            AlExtension.ColorizeWriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $"/ ^初期化しました。", 
                new [] {ConsoleColor.DarkGray, ConsoleColor.DarkBlue, ConsoleColor.DarkGreen}, false);
            // AlConsole.WriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $" ┃  ┗ 初期化しました。");
        }

    }
    
}