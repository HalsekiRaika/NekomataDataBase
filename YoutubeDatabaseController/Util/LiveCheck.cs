using System;
using System.Text.RegularExpressions;
using YoutubeDatabaseController.Scheme;

namespace YoutubeDatabaseController.Util {
    public static class LiveCheck {
        public static bool IsFreeChat(Item target) {
            return Regex.IsMatch(target.Snippet.Title, "(フリーチャット|ふり[ー～]ちゃっと|freechat|free chat|みんなの交流場)", RegexOptions.IgnoreCase);
        }

        public static bool IsFinishedLive(RefactorScheme scheme) {
            DateTime time = DateTime.Parse(scheme.StartTime);
            return time.Ticks - DateTime.Now.Ticks < 0;
        }
    }
}