using System.Collections.Generic;
using MongoDB.Driver;
using YoutubeDatabaseController.ChannelDictionary;
using YoutubeDatabaseController.Scheme;

namespace YoutubeDatabaseController {
    public class DataBaseCollection {
        public static void Insert(MongoClient client, List<RefactorScheme> schemeList) {
            
            // Production DataBase
            IMongoDatabase databaseHololive  = client.GetDatabase(Settings.HoloLive);
            IMongoDatabase databaseNijisanji = client.GetDatabase(Settings.NijiSanji);
            IMongoDatabase databaseAnimare   = client.GetDatabase(Settings.AniMare);
            
            // HoloLive Channel Collection
            IMongoCollection<RefactorScheme> collectionMatsuriChannel =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.NatsuiroMatsuri);
            IMongoCollection<RefactorScheme> collectionSubaruChannel  = 
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.OozoraSubaru);
            IMongoCollection<RefactorScheme> collectionAkirozeChannel =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.Akirosenthal);
            IMongoCollection<RefactorScheme> collectionHaatoChannel   =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.AkaiHaato);
            IMongoCollection<RefactorScheme> collectionAyameChannel   =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.NakiriAyame);
            IMongoCollection<RefactorScheme> collectionKoroneChannel  =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.InugamiKorone);
            IMongoCollection<RefactorScheme> collectionFlareChannel   =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.SiranuiFlare);
            IMongoCollection<RefactorScheme> collectionTowaChannel    =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.TokoyamiTowa);
            IMongoCollection<RefactorScheme> collectionPekoraChannel  =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.UsadaPekora);
            IMongoCollection<RefactorScheme> collectionAquaChannel    =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.MinatoAqua);
            IMongoCollection<RefactorScheme> collectionCocoChannel    =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.KiryuuCoco);
            IMongoCollection<RefactorScheme> collectionSuiseiChannel  =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.HoshimachiSuisei);
            IMongoCollection<RefactorScheme> collectionOkayuChannel   =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.NekomataOkayu);
            IMongoCollection<RefactorScheme> collectionWatameChannel  =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.TsunomakiWatame);
            IMongoCollection<RefactorScheme> collectionRushiaChannel  =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.UruhaRushia);
            IMongoCollection<RefactorScheme> collectionNoelChannel    =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.ShiroganeNoel);
            IMongoCollection<RefactorScheme> collectionMelChannel     =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.YozoraMel);
            IMongoCollection<RefactorScheme> collectionRobocoChannel  =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.Roboco);
            IMongoCollection<RefactorScheme> collectionSoraChannel    =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.TokinoSora);
            IMongoCollection<RefactorScheme> collectionFubukiChannel  =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.SirakamiFubuki);
            IMongoCollection<RefactorScheme> collectionMioChannel     =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.OokamiMio);
            IMongoCollection<RefactorScheme> collectionShionChannel   =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.MurasakiShion);
            IMongoCollection<RefactorScheme> collectionKanataChannel  =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.AmaneKanata);
            IMongoCollection<RefactorScheme> collectionChocoChannel   =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.YuzukiChoco);
            IMongoCollection<RefactorScheme> collectionMarineChannel  =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.HoushouMarine);
            IMongoCollection<RefactorScheme> collectionLunaChannel    =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.HimemoriLuna);
            IMongoCollection<RefactorScheme> collectionMikoChannel    =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.SakuraMiko);
            IMongoCollection<RefactorScheme> collectionAZKiChannel =
                databaseHololive.GetCollection<RefactorScheme>(ProductionHoloLive.AZKi);
            // ==== HoloLive Collection End Line ===
            
            schemeList.ForEach(schemes => {
                string searchedObject = ProductionHoloLive.GetChannelId(schemes.ChannelId);
                
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
                } else if (ProductionHoloLive.SiranuiFlare     == searchedObject) {
                    collectionFlareChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.TokoyamiTowa     == searchedObject) {
                    collectionTowaChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.UsadaPekora      == searchedObject) {
                    collectionPekoraChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.MinatoAqua       == searchedObject) {
                    collectionAquaChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.KiryuuCoco       == searchedObject) {
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
                } else if (ProductionHoloLive.SirakamiFubuki   == searchedObject) {
                    collectionFubukiChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.OokamiMio        == searchedObject) {
                    collectionMioChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.MurasakiShion    == searchedObject) {
                    collectionShionChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.AmaneKanata      == searchedObject) {
                    collectionKanataChannel.InsertOne(schemes);
                } else if (ProductionHoloLive.YuzukiChoco      == searchedObject) {
                    collectionChocoChannel.InsertOne(schemes);
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
            
        }
        
    }
    
}