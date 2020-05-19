using System;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Log5RLibs.Services;
using YoutubeDatabaseController.Scheme.LogScheme;
using YoutubeDatabaseController.Util;

namespace YoutubeDatabaseController {
    public class YoutubeAPIResponce {
        public static async Task<string> requestAsync(HttpClient client, string channelId) {
            Uri url = new Uri("https://www.googleapis.com/youtube/v3/search")
                .AddQuery("part", "snippet")
                .AddQuery("channelId", channelId)
                .AddQuery("type", "video")
                .AddQuery("eventType", "upcoming")
                .AddQuery("key", getToken());
            AlConsole.WriteLine(DefaultScheme.REQUEST_SCHEME, url.ToString());
            string result = await client.GetStringAsync(url);
            return result;
        }

        public static string getToken() {
            StreamReader reader = EnvironmentCheck.IsLinux()
                ? new StreamReader("/home/ubuntu")
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
    }
}