using System.Collections.Generic;
using YoutubeDatabaseController.Scheme;
using YtDataCollector.Scheme;

namespace YoutubeDatabaseController.List {
    public static class ListAggregation {
        // List
        private static List<string> _listResult     = new List<string>();
        private static List<string> _listVideoId    = new List<string>();
        
        // Dictionary
        private static Dictionary<string, Item>       _dictJsonScheme  = new Dictionary<string, Item>();
        private static Dictionary<string, ExtendItem> _dictTimeScheme  = new Dictionary<string, ExtendItem>();

        public static void SetJsonSchemeDict(JsonScheme scheme) {
            foreach (Item t in scheme.Items) {
                _listVideoId.Add(t.Id.VideoId);
                _dictJsonScheme.Add(t.Id.VideoId, t);
            }
        }

        public static void SetTimeScheme(StartTimeScheme scheme) {
            foreach (ExtendItem item in scheme.Items) {
                _dictTimeScheme.Add(item.Id, item);
            }
        }

        public static void SetResultList(string targetResult) {
            _listResult.Add(targetResult);
        }

        #region GETTER_REGION

        public static List<string> GetResultList() {
            return _listResult;
        }

        public static List<string> GetVideoIdList() {
            return _listVideoId;
        }
        
        public static Dictionary<string, Item> GetJsonSchemeList() {
            return _dictJsonScheme;
        }

        public static Dictionary<string, ExtendItem> GetTimeScheme() {
            return _dictTimeScheme;
        }

        #endregion

        #region LIST_INITIALIZER_REGION

        public static void ResultListInit() {
            _listResult.Clear();
        }

        #endregion

    }
    
}