using System.Collections.Generic;
using Log5RLibs.Services;
using YoutubeDatabaseController.Scheme;
using YoutubeDatabaseController.Scheme.LogScheme;
using YoutubeDatabaseController.Util;

namespace YoutubeDatabaseController {
    public class SchemeOrthopedy {
        private static List<RefactorScheme> _schemes = new List<RefactorScheme>();
        public static void Modification(Item[] targetArray) {
            foreach (Item t in targetArray) {
                //Checker
                RefactorScheme refactorScheme = new RefactorScheme() {
                    Title         = t.Snippet.Title,
                    Description   = t.Snippet.Description,
                    ChannelName   = t.Snippet.ChannelTitle,
                    ChannelId     = t.Snippet.ChannelId,
                    Thumbnail     = new ThumbnailsData() {
                        Url       = t.Snippet.Thumbnails.Default.Url,
                        Height    = t.Snippet.Thumbnails.Default.Height.ToString(),
                        Width     = t.Snippet.Thumbnails.Default.Width.ToString()
                    },
                    Publish       = t.Snippet.PublishTime.ToString(),
                };
                if (!LiveCheck.IsFreeChat(t)) {
                    _schemes.Add(refactorScheme);
                } else {
                    AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, "フリーチャット専用枠を検出し、除外しました。");
                    AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, $"対象：{t.Snippet.Title}");
                }
            }
        }
        
        public static void BundleModification(Dictionary<JsonScheme, StartTimeScheme> bundleScheme) {
            int count = -1;
            foreach (KeyValuePair<JsonScheme, StartTimeScheme> itemValue in bundleScheme) {
                RefactorScheme refactorScheme = new RefactorScheme() {
                    Title         = itemValue.Key.Items[++count].Snippet.Title,
                    Description   = itemValue.Key.Items[++count].Snippet.Description,
                    ChannelName   = itemValue.Key.Items[++count].Snippet.ChannelTitle,
                    ChannelId     = itemValue.Key.Items[++count].Snippet.ChannelId,
                    Thumbnail     = new ThumbnailsData() {
                        Url       = itemValue.Key.Items[++count].Snippet.Thumbnails.Default.Url,
                        Height    = itemValue.Key.Items[++count].Snippet.Thumbnails.Default.Height.ToString(),
                        Width     = itemValue.Key.Items[++count].Snippet.Thumbnails.Default.Width.ToString()
                    },
                    StartTime     = itemValue.Value.Items[++count].Details.ScheduledStartTime,
                    Publish       = itemValue.Key.Items[++count].Snippet.PublishTime.ToString(),
                };
                if (!LiveCheck.IsFreeChat(itemValue.Key.Items[++count])) {
                    _schemes.Add(refactorScheme);
                } else {
                    AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, "フリーチャット専用枠を検出し、除外しました。");
                    AlConsole.WriteLine(DefaultScheme.SORTLOG_SCHEME, $"対象：{itemValue.Key.Items[++count].Snippet.Title}");
                }

                count++;
            }
        }

        public static List<RefactorScheme> GetSchemes() {
            return _schemes;
        }
    }
}