using System;
using System.Collections.Generic;
using System.Text;
using YoutubeDatabaseController.Scheme;

namespace YoutubeDatabaseController.Util {
    public class VideoIdPackager {
        public static Dictionary<int, List<string>> Bundle(Item[] targetItems) {
            List<string> bundledList = new List<string>();
            Dictionary<int, List<string>> bundledDimension = new Dictionary<int, List<string>>();
            if (targetItems.Length >= 50) {
                List<string> dimBuffer = new List<string>();
                int calcLimit = LengthCalculate(targetItems.Length);
                for (int i = 0; i < calcLimit; i++) {
                    dimBuffer.Clear();
                    for (int j = 0; ; j++) {
                        dimBuffer.Add(targetItems[j].Id.VideoId);
                        if (i == calcLimit && j == SurplusCalculate(targetItems.Length)) {
                            bundledDimension.Add(i, dimBuffer);
                            break;
                        }
                        if (j == 50) {
                            bundledDimension.Add(i, dimBuffer);
                            break;
                        }
                    }
                }

                return bundledDimension;

            } else {
                for (int i = 0; i < targetItems.Length; i++) {
                    bundledList.Add(targetItems[i].Id.VideoId);
                }

                bundledDimension.Add(0, bundledList);
                
                return bundledDimension;
            }
        }

        private static string Join(List<string> targetList) {
            StringBuilder builder = new StringBuilder();
            targetList.ForEach(id => {
                builder.Append(id + ",");
            });
            return builder.ToString();
        }

        private static int LengthCalculate(int target) {
            return target - 50 * (target / 50) >= 1 ? (target / 50) + 1 : (target / 50);
        }

        private static int SurplusCalculate(int target) {
            return (target % 50);
        }
    }
}