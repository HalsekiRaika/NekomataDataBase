namespace RavenLauncher {
    public static class StatusChecker {
        public static void MakeStatusDir() {
            Settings.StatInfo.Create();
        }
        
        public static bool IsRecoveryMode() {
            return Settings.RecvFile.Exists;
        }
    }
}