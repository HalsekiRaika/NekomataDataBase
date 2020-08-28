using System.Collections.Generic;
using Log5RLibs.Services;
using YoutubeDatabaseController.Scheme;
using YoutubeDatabaseController.Scheme.LogScheme;
using YoutubeDatabaseController.Util;

namespace YoutubeDatabaseController {
    public static class SchemeOrthopedy {
        private static List<RefactorScheme> _schemes = new List<RefactorScheme>();
        public static void BundleModification(Dictionary<Item, ExtendItem> bundleScheme) {
            foreach (KeyValuePair<Item, ExtendItem> itemValue in bundleScheme) {
                RefactorScheme refactorScheme = new RefactorScheme() {
                    _id           = GenUuid.Generate(),
                    Title         = itemValue.Key.Snippet.Title,
                    Description   = itemValue.Key.Snippet.Description,
                    ChannelName   = itemValue.Key.Snippet.ChannelTitle,
                    ChannelId     = itemValue.Key.Snippet.ChannelId,
                    Thumbnail     = new ThumbnailsData() {
                        Url       = itemValue.Key.Snippet.Thumbnails.Default.Url,
                        Height    = itemValue.Key.Snippet.Thumbnails.Default.Height.ToString(),
                        Width     = itemValue.Key.Snippet.Thumbnails.Default.Width.ToString()
                    },
                    StartTime     = itemValue.Value.Details.ScheduledStartTime,
                    Publish       = itemValue.Key.Snippet.PublishTime.ToString(),
                };
                if (!LiveCheck.IsFreeChat(itemValue.Key)) {
                    if (!LiveCheck.IsFinishedLive(refactorScheme)) {
                        _schemes.Add(refactorScheme);
                    } else {
                        AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, "既に終了したライブを検出し、除外しました。");
                        AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, $"対象：{itemValue.Key.Snippet.Title}");
                    }
                } else {
                    AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, "フリーチャット専用枠を検出し、除外しました。");
                    AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, $"対象：{itemValue.Key.Snippet.Title}");
                }
            }
        }

        public static List<RefactorScheme> GetSchemes() {
            return _schemes;
        }
    }
}