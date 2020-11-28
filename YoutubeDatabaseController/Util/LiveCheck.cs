using System;
using System.Text.RegularExpressions;
using YoutubeDatabaseController.Scheme;

namespace YoutubeDatabaseController.Util {
    public static class LiveCheck {
        public static bool IsFreeChat(Item target) {
            return Regex.IsMatch(target.Snippet.Title, 
                "(フリーチャット|ふり[ー～]ちゃっ[とつト]|freechat|free chat|みんなの交流場|ふり～～ちゃっと|予定表|チャリーフット|といれ|free room|フリーーーーーーーチャット|ふりーーちゃっと|ふり～だむちゃっと|お花見会場|ＣｈａｔＲｏｏｍ|交流（色んな意味で）の場|みんながおしゃべり|チャットルーム|小野町旅館宴会所|フリーチャッツ|ボイス・Tシャツ販売中)",
                RegexOptions.IgnoreCase
                );
        }

        public static bool IsFinishedLive(RefactorScheme scheme) {
            if (string.IsNullOrEmpty(scheme.StartTime)) {
                return false;
            }
            DateTime time = DateTime.Parse(scheme.StartTime);
            return time.Ticks - DateTime.Now.Ticks < 0;
        }

        public static bool IsLazyLive(ExtendItem exItem) {
            return exItem.Details.ActualStartTime == null;
        }
    }
}