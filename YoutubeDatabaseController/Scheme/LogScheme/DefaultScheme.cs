using Log5RLibs.Services;

namespace YoutubeDatabaseController.Scheme.LogScheme {
    public class DefaultScheme {
        private const int MAX_INDEX = -15;
        private static readonly string YOUTUBE_API    = $"{"Youtube API",MAX_INDEX}";
        private static readonly string SORTED         = $"{"Data Refactor",MAX_INDEX}";
        private static readonly string SERIALIZE      = $"{"Data Serialize",MAX_INDEX}";
        private static readonly string DB_INITIALIZE  = $"{"DB Initialize",MAX_INDEX}";
        private static readonly string DB_INSERT_DATA = $"{"DB DataInsert",MAX_INDEX}";
        public static readonly AlCConfigScheme REQUEST_SCHEME         = new AlCConfigScheme(0, "Requesting...  ", YOUTUBE_API);
        public static readonly AlCConfigScheme RESPONCE_SCHEME        = new AlCConfigScheme(0, "Responce!      ", YOUTUBE_API);
        public static readonly AlCConfigScheme SORTLOG_SCHEME         = new AlCConfigScheme(0, null, SORTED);
        public static readonly AlCConfigScheme SERIALIZELOG_SCHEME    = new AlCConfigScheme(0, null, SERIALIZE);
        public static readonly AlCConfigScheme DB_INITIALIZE_SCHEME   = new AlCConfigScheme(0, null, DB_INITIALIZE);
        public static readonly AlCConfigScheme DB_IN_DATA_SCHEME_STBY = new AlCConfigScheme(0, "Stand By...    ", DB_INSERT_DATA);
        public static readonly AlCConfigScheme DB_IN_DATA_SCHEME_COMP = new AlCConfigScheme(0, "Complete!      ", DB_INSERT_DATA);
    }
}