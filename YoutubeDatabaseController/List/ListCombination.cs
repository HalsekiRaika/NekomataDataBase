using System;
using System.Collections.Generic;
using Log5RLibs.Services;
using YoutubeDatabaseController.Scheme;
using YoutubeDatabaseController.Scheme.LogScheme;

namespace YoutubeDatabaseController.List {
    public static class ListCombination {
        /// <summary>
        /// @Deprecated
        /// </summary>
        public static class VideoId {
            private static Dictionary<int, List<string>> _bundledDimension = new Dictionary<int, List<string>>();
            
            public static void SetBundledDimension(string[] videoIds) {
                List<string> bundledList = new List<string>();
                if (videoIds.Length >= 50) {
                    int calcLimit = LengthCalculate(videoIds.Length);
                    for (int i = 0; i < calcLimit; i++) {
                        List<string> dimBuffer = new List<string>();
                        for (int j = 0; ; j++) {
                            dimBuffer.Add(videoIds[j]);
                            if (i == calcLimit - 1 && j == SurplusCalculate(videoIds.Length)) {
                                _bundledDimension.Add(i, dimBuffer);
                                break;
                            }
                            if (j == 50 - 1) {
                                _bundledDimension.Add(i, dimBuffer);
                                break;
                            }
                        }
                    }
                } else {
                    for (int i = 0; i < videoIds.Length; i++) {
                        bundledList.Add(videoIds[i]);
                    }
                    _bundledDimension.Add(0, bundledList);
                }
            }
            
            /// <returns> Dictionary (page -> int, VideoId -> List(string)) </returns>
            public static Dictionary<int, List<string>> GetBundledDimension() {
                return _bundledDimension;
            }
            
            private static int LengthCalculate(int target) { 
                return target - 50 * (target / 50) >= 1 ? (target / 50) + 1 : (target / 50);
            }
            
            private static int SurplusCalculate(int target) { 
                return (target % 50);
            }
        }
        
        public static class Scheme {
            private static Dictionary<Item, ExtendItem> _bundleDictionary = new Dictionary<Item, ExtendItem>();
        
            public static void SetBundleDict(Dictionary<string, Item> dictJsonScheme, Dictionary<string, ExtendItem> dictExtendItem) {
                foreach (KeyValuePair<string, Item> value in dictJsonScheme) {
                    try {
                        _bundleDictionary.Add(value.Value, dictExtendItem[value.Key]);
                    } catch (Exception e) {
                        AlConsole.WriteLine(DefaultScheme.SORTLOG_WARN_SCHEME, "結合する辞書内に対応する値が見つかりません。");
                    }
                }
            }

            public static Dictionary<Item, ExtendItem> GetBundleDict() {
                return _bundleDictionary;
            }
        }
    }
}