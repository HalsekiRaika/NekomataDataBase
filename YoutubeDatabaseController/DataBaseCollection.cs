using System;
using System.Collections.Generic;
using Log5RLibs.Services;
using MongoDB.Driver;
using YoutubeDatabaseController.ChannelDictionary;
using YoutubeDatabaseController.Scheme;
using YoutubeDatabaseController.Scheme.LogScheme;

namespace YoutubeDatabaseController {
    public static class DataBaseCollection {
        public static void Insert(MongoClient client, List<RefactorScheme> schemeList) {
            
            // Production DataBase
            IMongoDatabase databaseHololive  = client.GetDatabase(Settings.Hololive);
            IMongoDatabase databaseNijisanji = client.GetDatabase(Settings.Nijisanji);
            IMongoDatabase databaseAnimare   = client.GetDatabase(Settings.AniMare);
            
            Dictionary<string, IMongoCollection<RefactorScheme>> collections = new Dictionary<string, IMongoCollection<RefactorScheme>>() {
                {ProductionHoloLive.NatsuiroMatsuri, databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.NatsuiroMatsuri)},
                {ProductionHoloLive.OozoraSubaru   , databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.OozoraSubaru)},
                {ProductionHoloLive.Akirosenthal   , databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.Akirosenthal)},
                {ProductionHoloLive.AkaiHaato      , databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.AkaiHaato)},
                {ProductionHoloLive.NakiriAyame    , databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.NakiriAyame)}
            };
            
            // HoloLive Channel Collection
            IMongoCollection<RefactorScheme> collectionMatsuriChannel  =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.NatsuiroMatsuri);
            IMongoCollection<RefactorScheme> collectionSubaruChannel   = 
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.OozoraSubaru);
            IMongoCollection<RefactorScheme> collectionAkirozeChannel  =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.Akirosenthal);
            IMongoCollection<RefactorScheme> collectionHaatoChannel    =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.AkaiHaato);
            IMongoCollection<RefactorScheme> collectionAyameChannel    =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.NakiriAyame);
            IMongoCollection<RefactorScheme> collectionKoroneChannel   =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.InugamiKorone);
            IMongoCollection<RefactorScheme> collectionFlareChannel    =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.ShiranuiFlare);
            IMongoCollection<RefactorScheme> collectionTowaChannel     = 
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.TokoyamiTowa);
            IMongoCollection<RefactorScheme> collectionPekoraChannel   =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.UsadaPekora);
            IMongoCollection<RefactorScheme> collectionAquaChannel     =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.MinatoAqua);
            IMongoCollection<RefactorScheme> collectionCocoChannel     =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.KiryuCoco);
            IMongoCollection<RefactorScheme> collectionSuiseiChannel   =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.HoshimachiSuisei);
            IMongoCollection<RefactorScheme> collectionOkayuChannel    =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.NekomataOkayu);
            IMongoCollection<RefactorScheme> collectionWatameChannel   =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.TsunomakiWatame);
            IMongoCollection<RefactorScheme> collectionRushiaChannel   =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.UruhaRushia);
            IMongoCollection<RefactorScheme> collectionNoelChannel     =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.ShiroganeNoel);
            IMongoCollection<RefactorScheme> collectionMelChannel      =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.YozoraMel);
            IMongoCollection<RefactorScheme> collectionRobocoChannel   =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.Roboco);
            IMongoCollection<RefactorScheme> collectionSoraChannel     =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.TokinoSora);
            IMongoCollection<RefactorScheme> collectionFubukiChannel   =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.ShirakamiFubuki);
            IMongoCollection<RefactorScheme> collectionMioChannel      =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.OokamiMio);
            IMongoCollection<RefactorScheme> collectionShionChannel    =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.MurasakiShion);
            IMongoCollection<RefactorScheme> collectionKanataChannel   =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.AmaneKanata);
            IMongoCollection<RefactorScheme> collectionChocoChannel    =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.YuzukiChoco);
            IMongoCollection<RefactorScheme> collectionChocoSubChannel =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.YuzukiChocoSub);
            IMongoCollection<RefactorScheme> collectionMarineChannel   =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.HoushouMarine);
            IMongoCollection<RefactorScheme> collectionLunaChannel     =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.HimemoriLuna);
            IMongoCollection<RefactorScheme> collectionMikoChannel     =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.SakuraMiko);
            IMongoCollection<RefactorScheme> collectionAZKiChannel     =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.AZKi);
            // ==== HoloLive Collection End Line ===
            
            // Start Initialization
            AlConsole.WriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $"初期化を開始します。");
            
            ProductionHoloLive.GetAllValue().ForEach(init => Initialization(databaseHololive, init));

            AlConsole.WriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $"全ての初期化が終了しました。");
            
            AlConsole.WriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $"初期化しました。");
            
            // Start Insert Data
            
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, "回収したデータをコレクションに格納します。");
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" ");
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" ================================================ ");
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $" ");
            
            schemeList.ForEach(schemes => {
                string searchedObject = ProductionHoloLive.GetChannelName(schemes.ChannelId);
                
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

                if (ProductionHoloLive.NatsuiroMatsuri         == searchedObject) {
                    collectionMatsuriChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.OozoraSubaru     == searchedObject) {
                    collectionSubaruChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.Akirosenthal     == searchedObject) {
                    collectionAkirozeChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.AkaiHaato        == searchedObject) {
                    collectionHaatoChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.NakiriAyame      == searchedObject) {
                    collectionAyameChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.InugamiKorone    == searchedObject) {
                    collectionKoroneChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.ShiranuiFlare    == searchedObject) {
                    collectionFlareChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.TokoyamiTowa     == searchedObject) {
                    collectionTowaChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.UsadaPekora      == searchedObject) {
                    collectionPekoraChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.MinatoAqua       == searchedObject) {
                    collectionAquaChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.KiryuCoco       == searchedObject) {
                    collectionCocoChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.HoshimachiSuisei == searchedObject) {
                    collectionSuiseiChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.NekomataOkayu    == searchedObject) {
                    collectionOkayuChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.TsunomakiWatame  == searchedObject) {
                    collectionWatameChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.UruhaRushia      == searchedObject) {
                    collectionRushiaChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.ShiroganeNoel    == searchedObject) {
                    collectionNoelChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.YozoraMel        == searchedObject) {
                    collectionMelChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.Roboco           == searchedObject) {
                    collectionRobocoChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.TokinoSora       == searchedObject) {
                    collectionSoraChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.ShirakamiFubuki  == searchedObject) {
                    collectionFubukiChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.OokamiMio        == searchedObject) {
                    collectionMioChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.MurasakiShion    == searchedObject) {
                    collectionShionChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.AmaneKanata      == searchedObject) {
                    collectionKanataChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.YuzukiChoco      == searchedObject) {
                    collectionChocoChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.YuzukiChocoSub   == searchedObject) {
                    collectionChocoSubChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.HoushouMarine    == searchedObject) {
                    collectionMarineChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.HimemoriLuna     == searchedObject) {
                    collectionLunaChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.SakuraMiko       == searchedObject) {
                    collectionMikoChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.AZKi             == searchedObject) {
                    collectionAZKiChannel.InsertOne(schemes);
                }

            });
            AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_COMP, "全ての挿入が成功しました。");
        }

        private static void Initialization(IMongoDatabase db, string targetCollection) {
            AlConsole.WriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $"対象のコレクションを初期化します。");
            AlConsole.WriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $"Collection: {targetCollection}");
            db.DropCollection(targetCollection);
            AlConsole.WriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $"初期化しました。");
        }

    }
    
}