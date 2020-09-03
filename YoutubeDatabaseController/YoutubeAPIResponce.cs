using System;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Log5RLibs.Services;
using YoutubeDatabaseController.Scheme.LogScheme;
using YoutubeDatabaseController.Util;

namespace YoutubeDatabaseController {
    public static class YoutubeAPIResponce {
        public static async Task<string> requestAsync(HttpClient client, string channelId) {
            Uri url = new Uri("https://www.googleapis.com/youtube/v3/search")
                .AddQuery("part", "snippet")
                .AddQuery("channelId", channelId)
                .AddQuery("type", "video")
                .AddQuery("eventType", "upcoming")
                .AddQuery("key", getToken());
            AlConsole.WriteLine(DefaultScheme.REQUEST_SCHEME, url.ToString().Substring(0, 129) + "[SECRET TOKEN]");
            string result = await client.GetStringAsync(url);
            return result;
        }

        public static async Task<string> RequestStartTimeAsync(HttpClient client, string[] videoId) {
            Uri url = new Uri("https://www.googleapis.com/youtube/v3/videos?")
                .AddQuery("part", "liveStreamingDetails")
                .AddQuery("key", getToken())
                .AddArrayQuery("id", videoId);
            AlConsole.WriteLine(DefaultScheme.REQUEST_SCHEME, "Extend Information Request...");
            foreach (string value in videoId) {
                AlConsole.WriteLine(DefaultScheme.REQUEST_SCHEME, $"  #-- {value, 15}");
            }
            return await client.GetStringAsync(url);
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
            UriBuilder builtUrl = new UriBuilder(uri);
            builtUrl.Query = collection.ToString();
            return builtUrl.Uri;
        }

        public static Uri AddArrayQuery(this Uri uri, string key, string[] valueArray) {
            NameValueCollection collection = HttpUtility.ParseQueryString(uri.Query);
            collection.Remove(key);
            collection.Add(key, ArrayToString(valueArray));
            UriBuilder builtUrl = new UriBuilder(uri);
            builtUrl.Query = collection.ToString();
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