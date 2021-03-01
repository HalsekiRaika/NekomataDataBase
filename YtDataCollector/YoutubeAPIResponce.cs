using System;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BaseDataCollector.Extension;
using Log5RLibs.Services;
using RetryExecutor;
using YoutubeDatabaseController;
using YtDataCollector.Scheme.LogScheme;

namespace YtDataCollector {
    public static class YoutubeAPIResponce {
        public static async Task<string> requestAsync(HttpClient client, string channelId) {
            Uri url = new Uri("https://www.googleapis.com/youtube/v3/search")
                .AddQuery("part", "snippet")
                .AddQuery("channelId", channelId)
                .AddQuery("type", "video")
                .AddQuery("eventType", "upcoming")
                .AddQuery("key", getToken());
            // AlConsole.WriteLine(DefaultScheme.REQUEST_SCHEME, url.ToString().Substring(0, 129) + "[SECRET TOKEN]");
            AlExtension.ColorizeWrite(DefaultScheme.REQUEST_SCHEME, $"情報をリクエスト ^: ^ChannelId [ ^{channelId, -16} ^] ^/ ",
                new [] {ConsoleColor.Green, ConsoleColor.DarkGray, ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.DarkGray});
            string result = ReSpell.Execute(5, 5, 
                () => client.GetStringAsync(url).Result, 
                (c) => {
                    Console.Write($"Retry ({c})");
                    Console.CursorLeft = 124;
                    Settings.UseQuota += 101;
                }, (c) => {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failure  \n");
                    Console.ResetColor();
                });
            Console.WriteLine("Success.");
            Settings.UseQuota += 101;
            return result;
        }

        public static async Task<string> RequestStartTimeAsync(HttpClient client, string videoIds /*string[] videoId */) {
            Uri url = new Uri("https://www.googleapis.com/youtube/v3/videos?")
                .AddQuery("part", "liveStreamingDetails")
                .AddQuery("key", getToken())
                .AddQuery("id", videoIds);
                //.AddArrayQuery("id", videoId);
                string res = ReSpell.Execute(5, 5, 
                () => client.GetStringAsync(url).Result, 
                (c) => AlConsole.WriteLine(DefaultScheme.REQUEST_SCHEME, $"Retry Request... [ Count: {c}]"));
            //foreach (string value in videoId) {
            //    AlConsole.WriteLine(DefaultScheme.REQUEST_SCHEME, $"  #-- {value, 15}");
            //}
            Settings.UseQuota += 8;
            return res;
        }

        private static string getToken() {
            OperatingSystem operatingSystem = Environment.OSVersion;
            StreamReader reader = operatingSystem.Platform != PlatformID.Win32NT
                ? new StreamReader("/home/ubuntu/APIToken.txt")
                : new StreamReader("C:\\Token\\YoutubeAPIToken.txt");
            return reader.ReadToEnd();
        }
    }

    public static class HttpExtension {
        public static Uri AddQuery(this Uri uri, string key, string value) {
            NameValueCollection collection = HttpUtility.ParseQueryString(uri.Query);
            collection.Remove(key);
            collection.Add(key, value);
            UriBuilder builtUrl = new UriBuilder(uri) {
                Query = collection.ToString(),
            };
            return builtUrl.Uri;
        }

        public static Uri AddArrayQuery(this Uri uri, string key, string[] valueArray) {
            NameValueCollection collection = HttpUtility.ParseQueryString(uri.Query);
            collection.Remove(key);
            collection.Add(key, ArrayToString(valueArray));
            UriBuilder builtUrl = new UriBuilder(uri) {
                Query = collection.ToString(),
            };
            return builtUrl.Uri;
        }

        private static string ArrayToString(string[] valueArray) {
            StringBuilder builder = new StringBuilder();
            foreach (string valueItem in valueArray) {
                builder.Append(valueItem + ",");
            }
            return builder.ToString();
        }
    }
}