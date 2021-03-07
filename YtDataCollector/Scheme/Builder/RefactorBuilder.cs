using YtDataCollector.Util;

namespace YtDataCollector.Scheme.Builder {
    public interface INeedTitle       { INeedDescription SetTitle(string title); }
    public interface INeedDescription { INeedVideoId SetDescription(string description); }
    public interface INeedVideoId     { INeedChannelId SetVideoId(string videoId); }
    public interface INeedChannelId   { INeedChannelName SetChannelId(string channelId); }
    public interface INeedChannelName { INeedPublish SetChannelName(string channelName); }
    public interface INeedPublish     { INeedStartTime SetPublish(string publish); }
    public interface INeedStartTime   { INeedThumbnail SetStartTime(string startTime); }
    public interface INeedThumbnail   { RefactorBuilder SetThumbnailData(YtDataCollector.Scheme.ThumbnailsData data); }

    public sealed class RefactorBuilder : INeedTitle, INeedDescription, INeedVideoId, INeedChannelId, 
        INeedChannelName, INeedPublish, INeedStartTime, INeedThumbnail {
        internal string _id         { get; private set; } = GenUuid.Generate();
        internal string Title       { get; private set; }
        internal string Description { get; private set; }
        internal string VideoId     { get; private set; }
        internal string ChannelId   { get; private set; }
        internal string ChannelName { get; private set; }
        internal string Publish     { get; private set; }
        internal string StartTime   { get; private set; }
        internal YtDataCollector.Scheme.ThumbnailsData Thumbnail { get; private set; }

        private RefactorBuilder() { }

        public static INeedTitle Define { get { return new RefactorBuilder(); } }

        public RefactorBuilder SetUUID(string id) {
            this._id = id;
            return this;
        }

        public INeedDescription SetTitle(string title) {
            this.Title = title;
            return this;
        }

        public INeedVideoId SetDescription(string description) {
            this.Description = description;
            return this;
        }

        public INeedChannelId SetVideoId(string videoId) {
            this.VideoId = videoId;
            return this;
        }

        public INeedChannelName SetChannelId(string channelId) {
            this.ChannelId = channelId;
            return this;
        }

        public INeedPublish SetChannelName(string channelName) {
            this.ChannelName = channelName;
            return this;
        }

        public INeedStartTime SetPublish(string publish) {
            this.Publish = publish;
            return this;
        }

        public INeedThumbnail SetStartTime(string startTime) {
            this.StartTime = startTime;
            return this;
        }

        public RefactorBuilder SetThumbnailData(YtDataCollector.Scheme.ThumbnailsData data) {
            this.Thumbnail = data;
            return this;
        }

        public YtDataCollector.Scheme.RefactorScheme Build() {
            return new YtDataCollector.Scheme.RefactorScheme(this);
        }
    }
}