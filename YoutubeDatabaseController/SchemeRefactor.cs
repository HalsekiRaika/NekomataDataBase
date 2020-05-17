using System.Collections.Generic;
using Log5RLibs.Services;
using YoutubeDatabaseController.Scheme;
using YoutubeDatabaseController.Scheme.LogScheme;
using YoutubeDatabaseController.Util;

namespace YoutubeDatabaseController {
    public class SchemeRefactor {
        private static List<RefactorScheme> _schemes = new List<RefactorScheme>();
        public static void Modification(Item[] targetArray) {
            foreach (Item t in targetArray) {
                //Chcker
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

        public static List<RefactorScheme> getSchemes() {
            return _schemes;
        }
    }
}