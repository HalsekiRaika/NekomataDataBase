using System;
using Newtonsoft.Json;

namespace YoutubeDatabaseController.Scheme {
    public partial class RefactorScheme {
        [JsonProperty("VideoTitle")]
        public string Title { get; set; }
        [JsonProperty("VideoDescription")]
        public string Description { get; set; }
        [JsonProperty("ChannelName")]
        public string ChannelName { get; set; }
        [JsonProperty("PublishTime")]
        public string Publish { get; set; }
        [JsonProperty("ThumbnailUrl")]
        public ThumbnailsData Thumbnail { get; set; } 
    }
    
    public partial class ThumbnailsData {
        [JsonProperty("ThumbnailImage")]
        public Uri Url { get; set; }
        
        [JsonProperty("width")]
        public string Width { get; set; }

        [JsonProperty("height")]
        public string Height { get; set; }
    }
}