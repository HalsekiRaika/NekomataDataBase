using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Log5RLibs.Services;
using MongoDB.Driver;
using Newtonsoft.Json;
using YoutubeDatabaseController.Extension;
using YoutubeDatabaseController.List;
using YoutubeDatabaseController.Scheme;
using YoutubeDatabaseController.Scheme.LogScheme;
using YoutubeDatabaseController.Util;
using static YoutubeDatabaseController.Scheme.LogScheme.DefaultScheme;

namespace YoutubeDatabaseController {
    public static class ControlMain {
        private static List<string> serializedObject = new List<string>();
        private static MongoClient _mongoClient;
        // ReSharper disable all PossibleMultipleEnumeration
        static void Main(string[] args) {
            Console.CursorVisible = false;
            Settings.StartupTime = DateTime.Now;
            AlExtension.ArrayWrite(START_UP, Settings.Startup);

            ArgumentParser.Decomposition(args);

            //Thread.Sleep(50000);

            // Set Client for Environment (Windows or Linux).
            _mongoClient = Settings.isLocal
                ? new MongoClient($"mongodb://{Settings.User}:{Settings.Pass}@124.0.0.1")
                : new MongoClient($"mongodb://{Settings.User}:{Settings.Pass}@{Settings.NekomataAws}");
            
            ConfigLoader.OnLoadEvent(_mongoClient);
            
            // Set Http Client. (For Reuse)
            // HttpClient isn't disposable, but is designed to "For Reuse".
            HttpClient httpClient = new HttpClient();

            // Send Request to YoutubeAPI.
            LoadedComponent.GetChannelId().ForEach(channelId => {
                string result = Task.Run(() => YoutubeAPIResponce.requestAsync(httpClient, channelId)).Result;
                //Thread.Sleep(3000);
                ListAggregation.SetResultList(result);
            });
            
            //ProductionHoloLive.GetAllKey().ForEach(channelId => {
            //    string result = Task.Run(() => YoutubeAPIResponce.requestAsync(httpClient, channelId)).Result;
            //    ListAggregation.SetResultList(result);
            //});

            // Finish Message
            AlConsole.WriteLine(DefaultScheme.RESPONCE_SCHEME, $"Success. Number of Request ({ListAggregation.GetResultList().Count})");

            // Youtube Response Json Deserialize
            ListAggregation.GetResultList().ForEach(result => {
                JsonScheme scheme = JsonConvert.DeserializeObject<JsonScheme>(result);
                ListAggregation.SetJsonSchemeDict(scheme);
            });
            
            // Result List Re-Initialize.
            ListAggregation.ResultListInit();

            IEnumerable<string> videoIdEnum = ListAggregation.GetVideoIdList().AsEnumerable<string>();
            
            // Because it is possible to save Quota,
            // When I search for 50 items in a batch, I can add 50 VideoId to the Dictionary<page number(int), VideoId(string)> as a lump organized into.
            // Send Request to YoutubeAPI. (Start Time for ScheduleLive)
            AlConsole.WriteLine(DefaultScheme.REQUEST_SCHEME, "Extend Information Request...");
            while (videoIdEnum.Any()) {
                string requestIds = String.Join(",", videoIdEnum.Take<string>(50));
                string[] sliced = videoIdEnum.ToArray<string>();
                string startTime = Task.Run(() => YoutubeAPIResponce.RequestStartTimeAsync(httpClient, requestIds)).Result;
                for (int i = 0; i < (sliced.Length % 50 > 0 ? sliced.Length / 50 + 1 : sliced.Length / 50); i++) {
                    for (int j = 0; j < 5; j++) {
                        AlExtension.ColorizeWrite(REQUEST_SCHEME, $"  #-- ^[ ", new [] {ConsoleColor.DarkGray, ConsoleColor.Green});
                        AlExtension.ColorizeWrite(REQUEST_SCHEME, String.Join(", ", sliced.Take(5)), 
                            new [] {ConsoleColor.Magenta}, false);
                        AlExtension.ColorizeWriteLine(REQUEST_SCHEME, $" ]", new [] {ConsoleColor.Green}, false);
                        sliced = sliced.Skip<string>(5).ToArray();
                    }
                }
                // foreach (string splitValue in requestIds.Split(",")) {
                //     AlConsole.WriteLine(DefaultScheme.REQUEST_SCHEME, $"  #-- {splitValue, 15}");
                // }
                // string startTime = Task.Run(() => YoutubeAPIResponce.RequestStartTimeAsync(httpClient, requestIds)).Result;
                ListAggregation.SetResultList(startTime);
                videoIdEnum = videoIdEnum.Skip<string>(50);
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
            /*
            ReSpell.Execute(5, 5, 
                () => { SchemeOrthopedy.BundleModification(ListCombination.Scheme.GetBundleDict()); }, (count) => {
                    AlConsole.Write(AlStatusEnum.Caution, $"{"TASK RESUMED", -15}", $"{"Data Bundler", -15}",
                        "Failed to execute the task. Task will try again.");
                    Console.CursorLeft = 0;
                }, (count) => {
                    AlConsole.WriteLine(AlStatusEnum.Error, $"{"TASK FAILURE", -15}", $"{"Data Bundler", -15}", 
                        $"Failed to execute the task. TryCount: {count, -10}\n");
                }); */
            SchemeOrthopedy.BundleModification(ListCombination.Scheme.GetBundleDict());

            AlConsole.WriteLine(SORTLOG_SCHEME, $"{" ", -63}");
            AlConsole.WriteLine(SORTLOG_SCHEME, "既に終了していたライブ : 以下のものは挿入タスクから除外されます。");
            AlConsole.WriteLine(SORTLOG_SCHEME, "----------------------------- 対象 -----------------------------");
            foreach (KeyValuePair<string, string> liveData in SchemeOrthopedy.GetFinishedLivesDict()) {
                AlExtension.ColorizeWriteLine(SORTLOG_SCHEME, $"[ ^{liveData.Key} ^] => \"^{liveData.Value}^\"",
                    new [] {ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Green, ConsoleColor.Gray, ConsoleColor.Green});
            }
            
            // foreach (string lives in SchemeOrthopedy.GetFinishedLives()) {
            //     AlConsole.WriteLine(SORTLOG_SCHEME, lives);
            // }
            
            AlConsole.WriteLine(SORTLOG_SCHEME, " ");
            AlConsole.WriteLine(SORTLOG_SCHEME, "フリーチャット専用枠 : 以下のものは挿入タスクから除外されます。");
            AlConsole.WriteLine(SORTLOG_SCHEME, "----------------------------- 対象 -----------------------------");
            foreach (KeyValuePair<string, string> liveData in SchemeOrthopedy.GetFreeChatLivesDict()) {
                AlExtension.ColorizeWriteLine(SORTLOG_SCHEME, $"[ ^{liveData.Key} ^] => \"^{liveData.Value}^\"",
                    new [] {ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Green, ConsoleColor.Gray, ConsoleColor.Green});
            }
            
            AlConsole.WriteLine(SORTLOG_SCHEME, " ");
            AlConsole.WriteLine(SORTLOG_SCHEME, "遅刻ライブ : 以下のものは挿入タスクに追加されます。");
            AlConsole.WriteLine(SORTLOG_SCHEME, "----------------------------- 対象 -----------------------------");
            if (SchemeOrthopedy.GetLazyLivesDict().Count != 0) {
                foreach (KeyValuePair<string, string> liveData in SchemeOrthopedy.GetLazyLivesDict()) {
                    AlExtension.ColorizeWriteLine(SORTLOG_SCHEME, $"[ ^{liveData.Key} ^] => \"^{liveData.Value}^\"",
                        new [] {ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Green, ConsoleColor.Gray, ConsoleColor.Green});
                }
            } else {
                AlConsole.WriteLine(SORTLOG_SCHEME, "遅刻ライブはありません。");
            }

            AlConsole.WriteLine(SORTLOG_SCHEME, " ");
            // foreach (string lives in SchemeOrthopedy.GetFreeChatLives()) {
            //     AlConsole.WriteLine(SORTLOG_SCHEME, lives);
            // }

            // Serialize the organized information.
            SchemeOrthopedy.GetSchemes().ForEach(i => {
                serializedObject.Add(JsonConvert.SerializeObject(i));
            });
            
            // Displays serialized information.
            serializedObject.ForEach(i => AlConsole.WriteLine(DefaultScheme.SERIALIZELOG_SCHEME, $"{i.ToString().Substring(0, 64), -67}" + "......[OMT]"));
            
            // Send the serialize object to Database.
            DataBaseCollection.Insert(_mongoClient, SchemeOrthopedy.GetSchemes());

            // Controller Task Finish Message
            long progressTicks = DateTime.Now.Ticks - Settings.StartupTime.Ticks;
            AlConsole.WriteLine(CONTROLLER, 
                $"Task Finished ! / {progressTicks} Ticks" +
                $" ({TimeSpan.FromTicks(progressTicks).Seconds} Sec)"
            );
            AlConsole.WriteLine(CONTROLLER, $"Number of Scheduled Live => {SchemeOrthopedy.GetSchemes().Count}");
            AlConsole.WriteLine(CONTROLLER, $"             Usage Quota => {Settings.UseQuota}");
            AlConsole.WriteLine(CONTROLLER, $"  Validated Caution Data => {Settings.CautData}");
            AlConsole.WriteLine(CONTROLLER, $"  Validated Warning Data => {Settings.WarnData}");
            AlConsole.WriteLine(CONTROLLER, "Have a good live broadcast today !");
        }
    }
}