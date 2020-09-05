using System;
using System.Collections.Generic;
using System.Linq;
using Log5RLibs.Services;
using MongoDB.Driver;
using YoutubeDatabaseController.ChannelDictionary;
using YoutubeDatabaseController.Scheme;
using YoutubeDatabaseController.Scheme.LogScheme;

namespace YoutubeDatabaseController {
    public static class DataBaseCollection {
        public static void Insert(MongoClient client, List<RefactorScheme> schemeList) {
            // Start Initialization
            AlConsole.WriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $"初期化を開始します。");

            foreach (KeyValuePair<IMongoDatabase, Dictionary<string, IMongoCollection<RefactorScheme>>> collection in LoadedComponent.GetAllCollections()) {
                AlConsole.WriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $" ┏ DataBase  : {collection.Key.DatabaseNamespace.DatabaseName}");
                foreach (KeyValuePair<string, IMongoCollection<RefactorScheme>> collectionDict in collection.Value) {
                    Initialization(collection.Key, collectionDict.Key);
                }
            }
            
            //ProductionHoloLive.GetAllValue().ForEach(init => Initialization(databaseHololive, init));

            AlConsole.WriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $" ┗ 全ての初期化が終了しました。");
            
            AlConsole.WriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $"初期化しました。");
            
            // Start Insert Data
            
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, "回収したデータをコレクションに格納します。");
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" ");
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" ================================================ ");
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" ");
            
            schemeList.ForEach(schemes => {
                string searchedObject = LoadedComponent.GetDataBaseName(schemes.ChannelId); //ProductionHoloLive.GetChannelName(schemes.ChannelId);
                
                AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, "以下のデータをコレクションに格納します。");
                AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $"生放送予定枠：");
                AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" => UUID          : {schemes._id}");
                AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" => タイトル　　  ：{schemes.Title}");
                AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" => チャンネル名  ：{schemes.ChannelName}");
                AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" => チャンネルID　：{schemes.ChannelId}");
                AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" ");
                AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" ================================================ ");
                AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" ");
                
                //collections[searchedObject].InsertOne(schemes);
                LoadedComponent.GetCollection()[searchedObject].InsertOne(schemes);
                
            });
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_COMP, "全ての挿入が成功しました。");
        }

        private static void Initialization(IMongoDatabase db, string targetCollection) {
            AlConsole.WriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $" ┃ 対象のコレクションを初期化します。");
            AlConsole.WriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $" ┣ Collection: {targetCollection}");
            db.DropCollection(targetCollection);
            AlConsole.WriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $" ┃  ┗ 初期化しました。");
        }

    }
    
}