using System.Collections.Generic;
using YoutubeDatabaseController.Scheme;

namespace YoutubeDatabaseController.Util {
    public class SortMixedData {
        public static Dictionary<JsonScheme, StartTimeScheme> ToMix(List<JsonScheme> jsonSchemes, Dictionary<string, StartTimeScheme> schemeDict) {
            int count = -1;
            Dictionary<JsonScheme, StartTimeScheme> mixDict =new Dictionary<JsonScheme, StartTimeScheme>();
            jsonSchemes.ForEach(scheme => {
                 mixDict.Add(scheme, schemeDict[scheme.Items[++count].Id.VideoId]);
            });
            return mixDict;
        }
    }
}