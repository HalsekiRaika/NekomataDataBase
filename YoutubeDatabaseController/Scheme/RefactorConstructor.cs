using YoutubeDatabaseController.Scheme.Builder;

namespace YoutubeDatabaseController.Scheme {
    public partial class RefactorScheme {
        public RefactorScheme(RefactorBuilder builder) {
            this._id         = builder._id;
            this.Title       = builder.Title;
            this.Description = builder.Description;
            this.VideoId     = builder.VideoId;
            this.ChannelId   = builder.ChannelId;
            this.ChannelName = builder.ChannelName;
            this.Publish     = builder.Publish;
            this.StartTime   = builder.StartTime;
            this.Thumbnail   = builder.Thumbnail;
        }
    }
    
    public partial class ThumbnailsData {
        public ThumbnailsData(ThumbnailDataBuilder builder) {
            this.Url = builder.Url;
            this.Width = builder.Width;
            this.Height = builder.Height;
        }
    }
}