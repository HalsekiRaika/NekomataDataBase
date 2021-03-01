using BaseDataCollector;
using Log5RLibs.Services;

namespace YtDataCollector.Scheme.LogScheme {
    public static class DefaultScheme {
        private const int MAX_INDEX = -15;
        private static readonly string CONTROLLER_RUN  = PaddingFactory("START UP");
        private static readonly string CONTROLLER_INFO = PaddingFactory("Controller");
        private static readonly string CONFIG_LOADER   = PaddingFactory("Config Loader");
        private static readonly string YOUTUBE_API     = PaddingFactory("Youtube API");
        private static readonly string SORTED          = PaddingFactory("Data Refactor");
        private static readonly string SERIALIZE       = PaddingFactory("Data Serialize");
        private static readonly string DB_INITIALIZE   = PaddingFactory("DB Initialize");
        private static readonly string DB_INSERT_DATA  = PaddingFactory("DB DataInsert");
        
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

        private static string PaddingFactory(string message) {
            return message.PadRight(GeneralSettings.GeneralPaddingNum, ' ');
        }
        
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