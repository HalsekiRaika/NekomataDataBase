using System;
using RavenLauncher.LiteLogger;

namespace RavenLauncher {
    public static class ArgParser {
        public static void Decomposition(string[] targetArgs) {
            if (targetArgs.Length < 1) {
                AlLite.WriteLine(WriteMode.CAUT, "Skipped config generate.");
                Settings.DoSkipConfigGenerate = true;
                return;
            }
            try { 
                for (int i = 0; i < targetArgs.Length; i++) {
                    switch (targetArgs[i]) {
                        case "--user":
                            Settings.DataBaseUserName = targetArgs[++i];
                            break;
                        
                        case "--pass":
                            Settings.DataBasePassWord = targetArgs[++i];
                            break;
                    }
                }
            } catch (IndexOutOfRangeException) {
                AlLite.WriteLine(WriteMode.ERR, "Incorrect Number of Arguments.");
            }
        }
    }
}