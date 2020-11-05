using System;
using System.Collections.Generic;
using Log5RLibs.Services;
using YoutubeDatabaseController.Scheme;
using YoutubeDatabaseController.Scheme.LogScheme;
using YoutubeDatabaseController.Util;

namespace YoutubeDatabaseController {
    public static class SchemeOrthopedy {
        private static List<RefactorScheme> _schemes = new List<RefactorScheme>();
        private static Dictionary<string, string> finishedLivesDict = new Dictionary<string, string>();
        private static Dictionary<string, string> freechatLivesDict = new Dictionary<string, string>();
        public static void BundleModification(Dictionary<Item, ExtendItem> bundleScheme) {
            foreach (KeyValuePair<Item, ExtendItem> itemValue in bundleScheme) {
                if (itemValue.Key == null || itemValue.Value == null) {
                    continue;
                }
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
                if (!LiveCheck.IsFreeChat(itemValue.Key)) {
                    if (!LiveCheck.IsFinishedLive(refactorScheme)) {
                        _schemes.Add(refactorScheme);
                    } else {
                        finishedLivesDict.Add(itemValue.Key.Id.VideoId, itemValue.Key.Snippet.Title);
                    }
                } else {
                    freechatLivesDict.Add(itemValue.Key.Id.VideoId, itemValue.Key.Snippet.Title);
                }
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
    }
}