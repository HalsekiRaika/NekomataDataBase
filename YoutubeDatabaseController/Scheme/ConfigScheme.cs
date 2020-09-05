namespace YoutubeDatabaseController.Scheme {
    public partial class ConfigScheme {
        public Profile Profile { get; set; }
        public ChannelData[] ChannelData { get; set; }
    }
    
    public partial class Profile {
        public string Name { get; set; }
        public string DBName { get; set; }
        public string Member { get; set; }
        public string Affiliation { get; set; }
        public string Twitter { get; set; }
    }
    
    public partial class ChannelData {
        public string SiteName { get; set; }
        public Details[] Details { get; set; }
    }
    
    public partial class Details {
        public object ID { get; set; }
    }
}