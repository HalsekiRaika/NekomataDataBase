using System.Text.RegularExpressions;
using YoutubeDatabaseController.Scheme;

namespace YoutubeDatabaseController.Util {
    public class LiveCheck {
        public static bool IsFreeChat(Item target) {
            return Regex.IsMatch(target.Snippet.Title, "(フリーチャット|ふり[ー～]ちゃっと|freechat|free chat)", RegexOptions.IgnoreCase);
        }
    }
}