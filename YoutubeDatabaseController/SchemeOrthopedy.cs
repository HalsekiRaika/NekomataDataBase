using System;
using System.Collections.Generic;
using YoutubeDatabaseController.Extension;
using YoutubeDatabaseController.Scheme;
using YoutubeDatabaseController.Scheme.Builder;
using YoutubeDatabaseController.Scheme.LogScheme;
using YoutubeDatabaseController.Util;

namespace YoutubeDatabaseController {
    public static class SchemeOrthopedy {
        private static readonly List<RefactorScheme> _schemes = new List<RefactorScheme>();
        private static readonly Dictionary<string, string> LazyLivesDict     = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> finishedLivesDict = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> freechatLivesDict = new Dictionary<string, string>();
        private static bool INeedAdditionalData = false;
        private static int count = 0;
        public static void BundleModification(Dictionary<Item, ExtendItem> bundleScheme) {
            foreach (KeyValuePair<Item, ExtendItem> itemValue in bundleScheme) {
                
                Item item;
                ExtendItem exItem;
                
                if (itemValue.Key.Snippet == null || itemValue.Value.Details == null) {
                    INeedAdditionalData = true;
                    Settings.CautData += 1;
                    continue;
                    // TODO : コラボ動画の追加処理を考える。
                } else {
                    item   = itemValue.Key;
                    exItem = itemValue.Value;
                }
                
                if (INeedAdditionalData) {
                    
                }
                
                AlExtension.ColorizeWrite(
                    DefaultScheme.CONTROLLER, $"({count, 3}/{bundleScheme.Count}) ^[ ^{itemValue.Key.Id.VideoId} ^] キューに追加しています / ",
                    new [] {ConsoleColor.White, ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Green});
                RefactorScheme refactorScheme = RefactorBuilder.Define
                    .SetTitle(item.Snippet.Title)
                    .SetDescription(item.Snippet.Description)
                    .SetVideoId(item.Id.VideoId)
                    .SetChannelId(item.Snippet.ChannelId)
                    .SetChannelName(item.Snippet.ChannelTitle)
                    .SetPublish(item.Snippet.PublishTime.ToString())
                    .SetStartTime(exItem.Details.ScheduledStartTime)
                    .SetThumbnailData(
                        ThumbnailDataBuilder.Define.SetUrl(new Uri($"https://i.ytimg.com/vi/{itemValue.Key.Id.VideoId}/maxresdefault_live.jpg")).Build())
                    .Build();
                
                Console.Write("Validate... ");
                /*
                RefactorScheme refactorScheme = new RefactorScheme() {
                    _id           = GenUuid.Generate(),
                    Title         = itemValue.Key.Snippet.Title,
                    Description   = itemValue.Key.Snippet.Description,
                    VideoId       = itemValue.Key.Id.VideoId,
                    ChannelName   = itemValue.Key.Snippet.ChannelTitle,
                    ChannelId     = itemValue.Key.Snippet.ChannelId,
                    Thumbnail     = new ThumbnailsData() {
                        Url       = new Uri($"https://i.ytimg.com/vi/{itemValue.Key.Id.VideoId}/maxresdefault_live.jpg"), //itemValue.Key.Snippet.Thumbnails.Medium.Url,
                        Height    = "641", //itemValue.Key.Snippet.Thumbnails.Medium.Height.ToString(),
                        Width     = "361"  //itemValue.Key.Snippet.Thumbnails.Medium.Width.ToString()
                    },
                    StartTime     = itemValue.Value.Details.ScheduledStartTime,
                    Publish       = itemValue.Key.Snippet.PublishTime.ToString(),
                };
                */
                
                if (!LiveCheck.IsFreeChat(itemValue.Key)) {
                    if (!LiveCheck.IsFinishedLive(refactorScheme)) {
                        _schemes.Add(refactorScheme);
                    } else {
                        if(LiveCheck.IsLazyLive(itemValue.Value)) {
                            _schemes.Add(refactorScheme);
                            LazyLivesDict.Add(item.Id.VideoId, item.Snippet.Title);
                        }
                        finishedLivesDict.Add(item.Id.VideoId, item.Snippet.Title);
                    }
                } else {
                    freechatLivesDict.Add(item.Id.VideoId, item.Snippet.Title);
                }
                
                Console.WriteLine("/ Finished");
                count++;
            }
        }

        public static List<RefactorScheme> GetSchemes() {
            return _schemes;
        }

        public static Dictionary<string, string> GetFinishedLivesDict() {
            return finishedLivesDict;
        }

        public static Dictionary<string, string> GetFreeChatLivesDict() {
            return freechatLivesDict;
        }

        public static Dictionary<string, string> GetLazyLivesDict() {
            return LazyLivesDict;
        }
    }
}