using System;
using System.Collections.Generic;
using YoutubeDatabaseController.Scheme;

namespace YoutubeDatabaseController {
    public class SchemeRefactor {
        private static List<RefactorScheme> _schemes = new List<RefactorScheme>();
        public static void Modification(Item[] targetArray) {
            foreach (Item t in targetArray) {
                RefactorScheme refactorScheme = new RefactorScheme() {
                    Title         = t.Snippet.Title,
                    Description   = t.Snippet.Description,
                    ChannelName   = t.Snippet.ChannelTitle,
                    Thumbnail = new ThumbnailsData() { Url = t.Snippet.Thumbnails.Default.Url },
                    Publish       = t.Snippet.PublishTime,
                };
                _schemes.Add(refactorScheme);
            }
        }

        public static List<RefactorScheme> getSchemes() {
            return _schemes;
        }
    }
}