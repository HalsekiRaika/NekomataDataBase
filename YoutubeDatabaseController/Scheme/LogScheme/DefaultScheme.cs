using Log5RLibs.Services;

namespace YoutubeDatabaseController.Scheme.LogScheme {
    public class DefaultScheme {
        private const int MAX_INDEX = -15;
        private static readonly string YOUTUBE_API = $"{"Youtube API",MAX_INDEX}";
        private static readonly string SORTED      = $"{"Data Refactor",MAX_INDEX}";
        private static readonly string SERIALIZE   = $"{"Data Serialize",MAX_INDEX}";
        public static readonly AlCConfigScheme REQUEST_SCHEME      = new AlCConfigScheme(0, "Requesting...", YOUTUBE_API);
        public static readonly AlCConfigScheme RESPONCE_SCHEME     = new AlCConfigScheme(0, "Responce!    ", YOUTUBE_API);
        public static readonly AlCConfigScheme SORTLOG_SCHEME      = new AlCConfigScheme(0, null, SORTED);
        public static readonly AlCConfigScheme SERIALIZELOG_SCHEME = new AlCConfigScheme(0, null, SERIALIZE);
    }
}