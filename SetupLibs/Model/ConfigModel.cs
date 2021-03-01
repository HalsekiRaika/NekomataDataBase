using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace SetupLibs.Model {
    public class ConfigModel {
        public string API_KEY { get; set; }
        public string DB_ACCESS_USERNAME { get; set; }
        public string DB_ACCESS_PASSWORD { get; set; }
        public bool   IS_LOCAL_MODE      { get; set; }
        [YamlMember(Alias = "IgnoreData")]
        public List<IgnoreData> ignoreData { get; set; }

        // ReSharper disable once ClassNeverInstantiated.Global
        public class IgnoreData {
            public string IgnoreDataName { get; set; }
            public string IgnoreVideoId  { get; set; }
        }
    }
}