using System;
using System.Collections.Generic;
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
        private static List<string> _resultList = new List<string>();
        private static List<string> serializedObject = new List<string>();
        private static List<JsonScheme> _schemeList = new List<JsonScheme>();
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
            
            _schemeList.ForEach(schemes => {
                SchemeRefactor.Modification(schemes.Items);
            });
            
            SchemeRefactor.GetSchemes().ForEach(i => {
                AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, "==============================================");
                AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, i.Title);
                AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, i.Description);
                AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, i.ChannelName);
                AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, i.Publish.ToString());
                AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, i.Thumbnail.Url.ToString());
                AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, "==============================================");
            });
            
            SchemeRefactor.GetSchemes().ForEach(i => {
                serializedObject.Add(JsonConvert.SerializeObject(i));
            });
            
            serializedObject.ForEach(i => AlConsole.WriteLine(DefaultScheme.SERIALIZELOG_SCHEME, i.ToString()));
            
            DataBaseCollection.Insert(_mongoClient, SchemeRefactor.GetSchemes());
            
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