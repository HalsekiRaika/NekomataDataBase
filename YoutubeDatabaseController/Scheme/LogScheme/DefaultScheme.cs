using System;
using Log5RLibs.Services;

namespace YoutubeDatabaseController.Scheme.LogScheme {
    public static class DefaultScheme {
        private const int MAX_INDEX = -15;
        private static readonly string CONTROLLER_RUN  = $"{"START UP"      , MAX_INDEX}";
        private static readonly string CONTROLLER_INFO = $"{"Controller"    , MAX_INDEX}";
        private static readonly string CONFIG_LOADER   = $"{"Config Loader" , MAX_INDEX}";
        private static readonly string YOUTUBE_API     = $"{"Youtube API"   , MAX_INDEX}";
        private static readonly string SORTED          = $"{"Data Refactor" , MAX_INDEX}";
        private static readonly string SERIALIZE       = $"{"Data Serialize", MAX_INDEX}";
        private static readonly string DB_INITIALIZE   = $"{"DB Initialize" , MAX_INDEX}";
        private static readonly string DB_INSERT_DATA  = $"{"DB DataInsert" , MAX_INDEX}";
        
        // Controller
        
        public static readonly AlCConfigScheme START_UP               = new AlCConfigScheme(0, null, CONTROLLER_RUN);
        public static readonly AlCConfigScheme CONTROLLER             = new AlCConfigScheme(0, null, CONTROLLER_INFO);

        // Youtube API
        public static readonly AlCConfigScheme REQUEST_SCHEME         = new AlCConfigScheme(0, "Requesting...  ", YOUTUBE_API);
        public static readonly AlCConfigScheme RESPONCE_SCHEME        = new AlCConfigScheme(0, "Response!      ", YOUTUBE_API);
        public static readonly AlCConfigScheme SORTLOG_SCHEME         = new AlCConfigScheme(0, null, SORTED);
        public static readonly AlCConfigScheme SORTLOG_WARN_SCHEME    = new AlCConfigScheme(2, null, SORTED);
        public static readonly AlCConfigScheme SERIALIZELOG_SCHEME    = new AlCConfigScheme(0, null, SERIALIZE);
        public static readonly AlCConfigScheme DB_INITIALIZE_SCHEME   = new AlCConfigScheme(0, null, DB_INITIALIZE);
        public static readonly AlCConfigScheme DB_IN_DATA_SCHEME_STBY = new AlCConfigScheme(0, "Stand By...    ", DB_INSERT_DATA);
        public static readonly AlCConfigScheme DB_IN_DATA_SCHEME_COMP = new AlCConfigScheme(0, "Complete!      ", DB_INSERT_DATA);
        
        // Config
        public static readonly AlCConfigScheme CONFIG_INFORMATION     = new AlCConfigScheme(0, "Loading...     ", CONFIG_LOADER);
        public static readonly AlCConfigScheme CONFIG_EXCEPTION       = new AlCConfigScheme(3, "StartUp Excp   ", CONFIG_LOADER);
        
        // Log5RLibs v2.0 LINE START
        /*
        public static readonly AlPreset START_UP = AlPresetBuilder.Define
            .SetStatusName(CONTROLLER_RUN).SetStatusColor(ConsoleColor.Green)
            .SetThreadName(CONTROLLER_INFO).SetThreadColor(ConsoleColor.Magenta)
            .Build();

        public static readonly AlPreset CONTROLLER = AlPresetBuilder.Define
            .SetStatusName()
            .Build();
            */
    }
}