﻿using System;
using System.Collections.Generic;
using BaseDataCollector.Extension;
using Log5RLibs.Services;
using MongoDB.Driver;
using YoutubeDatabaseController;
using YoutubeDatabaseController.Scheme;
using YtDataCollector.Scheme.LogScheme;

namespace YtDataCollector {
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
            
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, "以下の回収したデータをコレクションに格納します。");
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" + List");
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" +---------------------+---------------------+---------------------+ ");
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" | -> | UUID     | VideoId     | ChannelId                | ChannelName             ");

            schemeList.ForEach(scheme => {
                AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, 
                    $"   >>   {scheme._id.Substring(0, 8)} | " + 
                    $"{scheme.VideoId} | {scheme.ChannelId} | {scheme.ChannelName}"
                );
            });
            
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" +---------------------+---------------------+---------------------+ ");
            AlConsole.Write(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $"Inserting Data... [ 0   % ]");
            int progress = Math.Abs(100 / schemeList.Count);
            int count = 1;
            schemeList.ForEach(schemes => {
                Console.CursorLeft = 84;
                string searchedObject = LoadedComponent.GetDataBaseName(schemes.ChannelId);
                LoadedComponent.GetCollection()[searchedObject].InsertOne(schemes);
                Console.Write(progress * count);
                count++;
            });
            Console.Write("\n");
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_COMP, "全ての挿入が成功しました。");
        }

        private static void Initialization(IMongoDatabase db, string targetCollection) {
            AlExtension.ColorizeWrite(DefaultScheme.DB_INITIALIZE_SCHEME, $" ┣ ^Init Collection: ^{targetCollection, -26} ", 
                new [] {ConsoleColor.DarkGray, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Green});
            // AlConsole.WriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $" ┣ Init Collection: {targetCollection}");
            db.DropCollection(targetCollection);
            AlExtension.ColorizeWriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $"/ ^初期化しました。", 
                new [] {ConsoleColor.DarkGray, ConsoleColor.DarkBlue, ConsoleColor.DarkGreen}, false);
            // AlConsole.WriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $" ┃  ┗ 初期化しました。");
        }

    }
    
}