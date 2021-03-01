using Newtonsoft.Json;

namespace BaseDataCollector.Structure {
    public partial class StartTimeScheme {
        [JsonProperty("kind")]
        public string Kind { get; set; }
        [JsonProperty("etag")]
        public string Etag { get; set; }
        [JsonProperty("items")]
        public ExtendItem[] Items { get; set; }
        [JsonProperty("pageInfo")]
        public ExtendPageInfo PageInfo { get; set; }
    }
    
    public partial class ExtendItem {
        [JsonProperty("kind")]
        public string Kind { get; set; }
        [JsonProperty("etag")]
        public string Etag { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("liveStreamingDetails")]
        public LiveStreamingDetails Details { get; set; }
    }
    
    public partial class LiveStreamingDetails {
        [JsonProperty("actualStartTime")] 
        public string ActualStartTime { get; set; } = "";
        [JsonProperty("actualEndTime")] 
        public string ActualEndTime { get; set; } = "";
        [JsonProperty("scheduledStartTime")]
        public string ScheduledStartTime { get; set; }
        [JsonProperty("activeLiveChatId")]
        public string ActiveLiveChatId { get; set; }
    }
    
    public partial class ExtendPageInfo {
        [JsonProperty("totalResults")]
        public int TotalResults { get; set; }
        [JsonProperty("resultsPerPage")]
        public int ResultsPerPage { get; set; }
    }
}