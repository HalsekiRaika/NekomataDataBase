using System.Collections.Generic;
using Log5RLibs.Services;
using YoutubeDatabaseController.Scheme;
using YoutubeDatabaseController.Scheme.LogScheme;
using YoutubeDatabaseController.Util;

namespace YoutubeDatabaseController {
    public static class SchemeOrthopedy {
        private static List<RefactorScheme> _schemes = new List<RefactorScheme>();
        private static List<string> finishedLives = new List<string>();
        private static List<string> freechatLives = new List<string>();
        public static void BundleModification(Dictionary<Item, ExtendItem> bundleScheme) {
            foreach (KeyValuePair<Item, ExtendItem> itemValue in bundleScheme) {
                RefactorScheme refactorScheme = new RefactorScheme() {
                    _id           = GenUuid.Generate(),
                    Title         = itemValue.Key.Snippet.Title,
                    Description   = itemValue.Key.Snippet.Description,
                    VideoId       = itemValue.Key.Id.VideoId,
                    ChannelName   = itemValue.Key.Snippet.ChannelTitle,
                    ChannelId     = itemValue.Key.Snippet.ChannelId,
                    Thumbnail     = new ThumbnailsData() {
                        Url       = itemValue.Key.Snippet.Thumbnails.Medium.Url,
                        Height    = itemValue.Key.Snippet.Thumbnails.Medium.Height.ToString(),
                        Width     = itemValue.Key.Snippet.Thumbnails.Medium.Width.ToString()
                    },
                    StartTime     = itemValue.Value.Details.ScheduledStartTime,
                    Publish       = itemValue.Key.Snippet.PublishTime.ToString(),
                };
                if (!LiveCheck.IsFreeChat(itemValue.Key)) {
                    if (!LiveCheck.IsFinishedLive(refactorScheme)) {
                        _schemes.Add(refactorScheme);
                    } else {
                        finishedLives.Add($"[ {itemValue.Key.Id.VideoId} ] => \"{itemValue.Key.Snippet.Title}\"");
                    }
                } else {
                    freechatLives.Add($"[ {itemValue.Key.Id.VideoId} ] => \"{itemValue.Key.Snippet.Title}\"");
                }
            }
        }

        public static List<RefactorScheme> GetSchemes() {
            return _schemes;
        }

        public static List<string> GetFinishedLives() {
            return finishedLives;
        }

        public static List<string> GetFreeChatLives() {
            return freechatLives;
        }
    }
}