using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Log5RLibs.Services;
using MongoDB.Driver;
using Newtonsoft.Json;
using YoutubeDatabaseController.Scheme;
using YoutubeDatabaseController.Scheme.LogScheme;
using YoutubeDatabaseController.Util;

namespace YoutubeDatabaseController {
    public class ControlMain {
        private static List<string> serializedObject = new List<string>();
        static void Main(string[] args) {
            Console.WriteLine(Settings.StartupMessage);

            if (EnvironmentCheck.IsLinux()) {
                MongoClient mongoClient = new MongoClient("mongodb://124.0.0.1");
            } else {
                MongoClient mongoClient = new MongoClient("mongodb://192.168.0.5");
            }
            
            HttpClient httpClient = new HttpClient();
            string channelId = Console.ReadLine();
            string result = Task.Run(() => YoutubeAPIResponce.requestAsync(httpClient, channelId)).Result;
            AlConsole.WriteLine(DefaultScheme.RESPONCE_SCHEME, "Success.");
            Console.WriteLine(result);
            JsonScheme scheme = new JsonScheme();
            scheme = JsonConvert.DeserializeObject<JsonScheme>(result);
            SchemeRefactor.Modification(scheme.Items);
            SchemeRefactor.getSchemes().ForEach(i => {
                AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, "==============================================");
                AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, i.Title);
                AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, i.Description);
                AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, i.ChannelName);
                AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, i.Publish.ToString());
                AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, i.Thumbnail.Url.ToString());
                AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, "==============================================");
            });
            SchemeRefactor.getSchemes().ForEach(i => {
                serializedObject.Add(JsonConvert.SerializeObject(i));
            });
            serializedObject.ForEach(i => AlConsole.WriteLine(DefaultScheme.SERIALIZELOG_SCHEME, i.ToString()));
            Console.ReadKey();
        }
    }
}