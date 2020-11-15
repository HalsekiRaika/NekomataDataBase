using System;

namespace YoutubeDatabaseController.Scheme.Builder {
    public interface INeedThumbnailUrl { ThumbnailDataBuilder SetUrl(Uri uri); }

    public sealed class ThumbnailDataBuilder : INeedThumbnailUrl {
        internal Uri    Url    { get; private set; }
        internal string Width  { get; private set; } = "641";
        internal string Height { get; private set; } = "361";

        private ThumbnailDataBuilder() { }

        public static INeedThumbnailUrl Define { get { return new ThumbnailDataBuilder();} }

        public ThumbnailDataBuilder SetUrl(Uri uri) {
            this.Url = uri;
            return this;
        }

        public ThumbnailDataBuilder SetWidth(int widthValue) {
            this.Width = widthValue.ToString();
            return this;
        }

        public ThumbnailDataBuilder SetHeight(int heightValue) {
            this.Height = heightValue.ToString();
            return this;
        }

        public ThumbnailsData Build() {
            return new ThumbnailsData(this);
        }
    }
}