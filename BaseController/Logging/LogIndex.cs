using Log5RLibs.Services;

namespace BaseController.Logging {
    public static class LogIndex {
        private static readonly string CONFIG_LOADER   = PaddingFactory("Config Loader");
        // Config
        public static readonly AlCConfigScheme CONFIG_INFORMATION     = new AlCConfigScheme(0, "Loading...     ", CONFIG_LOADER);
        public static readonly AlCConfigScheme CONFIG_EXCEPTION       = new AlCConfigScheme(3, "StartUp Excp   ", CONFIG_LOADER);
        
        public static string PaddingFactory(string message) {
            return message.PadRight(GeneralSettings.GeneralPaddingNum, ' ');
        }
    }
}