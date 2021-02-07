using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace BaseController.Extension {
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