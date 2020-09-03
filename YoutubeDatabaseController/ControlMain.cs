using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Log5RLibs.Services;
using Log5RLibs.utils;
using MongoDB.Driver;
using Newtonsoft.Json;
using YoutubeDatabaseController.ChannelDictionary;
using YoutubeDatabaseController.Extension;
using YoutubeDatabaseController.List;
using YoutubeDatabaseController.Scheme;
using YoutubeDatabaseController.Scheme.LogScheme;
using YoutubeDatabaseController.Util;
using static YoutubeDatabaseController.Scheme.LogScheme.DefaultScheme;

namespace YoutubeDatabaseController {
    public static class ControlMain {
        private static List<string>     serializedObject = new List<string>();
        
        private static MongoClient _mongoClient;
        
        static void Main(string[] args) {
            AlExtension.ArrayWrite(START_UP, Settings.Startup);

            ArgumentParser.Decomposition(args);
            
            ConfigLoader.OnLoadEvent();
            
            //Thread.Sleep(50000);

            // Set Client for Environment (Windows or Linux).
            _mongoClient = Settings.isLocal
                ? new MongoClient($"mongodb://{Settings.User}:{Settings.Pass}@124.0.0.1")
                : new MongoClient($"mongodb://{Settings.User}:{Settings.Pass}@{Settings.NekomataAws}");
            
            // Set Http Client. (For Reuse)
            // HttpClient isn't disposable, but is designed to "For Reuse".
            HttpClient httpClient = new HttpClient();

            // Send Request to YoutubeAPI.
            ProductionHoloLive.GetAllKey().ForEach(channelId => {
                string result = Task.Run(() => YoutubeAPIResponce.requestAsync(httpClient, channelId)).Result;
                ListAggregation.SetResultList(result);
            });
            
            // Finish Message
            AlConsole.WriteLine(DefaultScheme.RESPONCE_SCHEME, "Success.");

            // Youtube Response Json Deserialize
            ListAggregation.GetResultList().ForEach(result => {
                JsonScheme scheme = JsonConvert.DeserializeObject<JsonScheme>(result);
                ListAggregation.SetJsonSchemeDict(scheme);
            });
            
            // Result List Re-Initialize.
            ListAggregation.ResultListInit();
            
            // Because it is possible to save Quota,
            // When I search for 50 items in a batch, I can add 50 VideoId to the Dictionary<page number(int), VideoId(string)> as a lump organized into.
            ListCombination.VideoId.SetBundledDimension(ListAggregation.GetVideoIdList().ToArray());

            // Send Request to YoutubeAPI. (Start Time for ScheduleLive)
            foreach (KeyValuePair<int, List<string>> bundledValue in ListCombination.VideoId.GetBundledDimension()) {
                string startTime = Task.Run(() => YoutubeAPIResponce.RequestStartTimeAsync(httpClient, bundledValue.Value.ToArray())).Result;
                ListAggregation.SetResultList(startTime);
            }
            
            // Finish Message
            AlConsole.WriteLine(DefaultScheme.RESPONCE_SCHEME, "Success.");

            // Youtube Response Json Deserialize
            ListAggregation.GetResultList().ForEach(result => {
                StartTimeScheme scheme = JsonConvert.DeserializeObject<StartTimeScheme>(result);
                ListAggregation.SetTimeScheme(scheme);
            });

            // Link JsonScheme and ExtendItem whose VideoId is the same.
            ListCombination.Scheme.SetBundleDict(ListAggregation.GetJsonSchemeList(), ListAggregation.GetTimeScheme());

            // Organize necessary information and put it into a RefactorScheme and store it in List(RefactorScheme).
            SchemeOrthopedy.BundleModification(ListCombination.Scheme.GetBundleDict());

            // Serialize the organized information.
            SchemeOrthopedy.GetSchemes().ForEach(i => {
                serializedObject.Add(JsonConvert.SerializeObject(i));
            });
            
            // Displays serialized information.
            serializedObject.ForEach(i => AlConsole.WriteLine(DefaultScheme.SERIALIZELOG_SCHEME, $"{i.ToString().Substring(0, 64), -67}" + "......[OMT]"));
            
            // Send the serialize object to Database.
            DataBaseCollection.Insert(_mongoClient, SchemeOrthopedy.GetSchemes());

            // Controller Task Finish Message
            AlConsole.WriteLine(CONTROLLER, "Task Finished !");
            AlConsole.WriteLine(CONTROLLER, "Have a good live broadcast today !");
        }
    }
}