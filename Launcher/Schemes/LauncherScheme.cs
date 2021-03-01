using Log5RLibs.Services;

namespace Launcher.Schemes {
    public static class LauncherScheme {
        private const int MAX_INDEX = -15;
        
        // Thread Name
        private static readonly string LauncherInfo     = $"{"Launcher"         , MAX_INDEX}";
        
        // Status Name
        private static readonly string StatCollect      = $"{"Collecting..."   , MAX_INDEX}";
        private static readonly string StatFailCollect  = $"{"Collect Failure" , MAX_INDEX}";
        private static readonly string StatRecvCollect  = $"{"Recovery Boot"   , MAX_INDEX}";

        // Schemes
        public static readonly AlCConfigScheme LauncherInfoScheme   = new AlCConfigScheme(0, null            , LauncherInfo);
        public static readonly AlCConfigScheme LauncherCautScheme   = new AlCConfigScheme(1, null            , LauncherInfo);
        public static readonly AlCConfigScheme RunCollectorScheme   = new AlCConfigScheme(0, StatCollect     , LauncherInfo);
        public static readonly AlCConfigScheme FailureCollectScheme = new AlCConfigScheme(3, StatFailCollect , LauncherInfo);
        public static readonly AlCConfigScheme RecoveryBootScheme   = new AlCConfigScheme(1, StatRecvCollect , LauncherInfo);
    }
}