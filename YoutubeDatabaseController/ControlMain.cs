using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Log5RLibs.Services;
using Log5RLibs.utils;
using MongoDB.Driver;
using Newtonsoft.Json;
using YoutubeDatabaseController.Scheme;
using YoutubeDatabaseController.Scheme.LogScheme;
using YoutubeDatabaseController.Util;

namespace YoutubeDatabaseController {
    public class ControlMain {
        
        //List
        private static List<string>     _resultList      = new List<string>();
        private static List<Item>       _willConvertList = new List<Item>();
        private static List<string>     serializedObject = new List<string>();
        private static List<JsonScheme> _schemeList      = new List<JsonScheme>();
        
        //Dictionary
        private static Dictionary<string, StartTimeScheme> _schemeExtList   = new Dictionary<string, StartTimeScheme>();
        private static Dictionary<int, List<string>>       BundledVideoId   = new Dictionary<int, List<string>>();
        private static Dictionary<JsonScheme, StartTimeScheme> BundledSchemes = new Dictionary<JsonScheme, StartTimeScheme>();
        
        private static MongoClient _mongoClient;
        
        static void Main(string[] args) {
            Console.WriteLine(Settings.StartupMessage);

            _mongoClient = EnvironmentCheck.IsLinux()
                ? new MongoClient("mongodb://124.0.0.1")
                : new MongoClient("mongodb://192.168.0.5");
            
            HttpClient httpClient = new HttpClient();
            ChannelIDList.GetChannelId().ForEach(channelId => {
                string result = Task.Run(() => YoutubeAPIResponce.requestAsync(httpClient, channelId)).Result;
                _resultList.Add(result);
            });
            
            AlConsole.WriteLine(DefaultScheme.RESPONCE_SCHEME, "Success.");

            _resultList.ForEach(result => {
                JsonScheme scheme = new JsonScheme();
                scheme = JsonConvert.DeserializeObject<JsonScheme>(result);
                _schemeList.Add(scheme);
            });
            
            _resultList.Clear();
            
            _schemeList.ForEach(schemes => {
                foreach (Item objItem in schemes.Items) {
                    _willConvertList.Add(objItem);
                }
            });

            BundledVideoId = VideoIdPackager.Bundle(_willConvertList.ToArray());

            foreach (KeyValuePair<int, List<string>> bundledValue in BundledVideoId) {
                string startTime = Task.Run(() => YoutubeAPIResponce.RequestStartTimeAsync(httpClient, bundledValue.Value.ToArray())).Result;
                _resultList.Add(startTime);
            }

            int count = -1;
            _resultList.ForEach(result => {
                StartTimeScheme scheme = new StartTimeScheme();
                scheme = JsonConvert.DeserializeObject<StartTimeScheme>(result);
                _schemeExtList.Add(scheme.Items[++count].Id, scheme);
                count++;
            });

            BundledSchemes = SortMixedData.ToMix(_schemeList, _schemeExtList);
            
            SchemeOrthopedy.BundleModification(BundledSchemes);
            
            /*
            _schemeList.ForEach(schemes => {
                SchemeOrthopedy.Modification(schemes.Items);
            });
            */
            
            SchemeOrthopedy.GetSchemes().ForEach(i => {
                AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, "==============================================");
                AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, i.Title);
                AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, i.Description);
                AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, i.ChannelName);
                AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, i.StartTime.ToString());
                AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, i.Publish.ToString());
                AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, i.Thumbnail.Url.ToString());
                AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, "==============================================");
            });
            
            SchemeOrthopedy.GetSchemes().ForEach(i => {
                serializedObject.Add(JsonConvert.SerializeObject(i));
            });
            
            serializedObject.ForEach(i => AlConsole.WriteLine(DefaultScheme.SERIALIZELOG_SCHEME, i.ToString()));
            
            DataBaseCollection.Insert(_mongoClient, SchemeOrthopedy.GetSchemes());
            
            /*
            //DB Insert
            IMongoDatabase database = _mongoClient.GetDatabase("TestCollection");
            IMongoCollection<RefactorScheme> collection = database.GetCollection<RefactorScheme>("upcoming");
            database.DropCollection("upcoming");
            AlConsole.WriteLine(DefaultScheme.DB_INITIALIZE_SCHEME, $"初期化しました。");
            SchemeRefactor.GetSchemes().ForEach(i => {
                AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, "以下のデータをデータベースに送信します。");
                AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_STBY, $"生放送予定枠：{i.Title}");
                collection.InsertOne(i);
                AlConsole.WriteLine(DefaultScheme.DB_IN_DATA_SCHEME_COMP, "成功しました。");
            });
            */
            
            AlConsole.WriteLine(AlStatusEnum.Information, null,"Controller", "Task Finished !");
            AlConsole.WriteLine(AlStatusEnum.Information, null,"Controller", "Have a good live broadcast today !");
        }
    }
}