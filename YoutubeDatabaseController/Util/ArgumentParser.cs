using System;
using Log5RLibs.Services;
using Log5RLibs.utils;

namespace YoutubeDatabaseController.Util {
    public static class ArgumentParser {
        public static void Decomposition(string[] targetArgs) {
            try {
                for (int i = 0; i < targetArgs.Length; i++) {
                    switch (targetArgs[i]) {
                        case "--user":
                            Settings.User = targetArgs[++i];
                            break;
                        
                        case "--pass":
                            Settings.Pass = targetArgs[++i];
                            break;
                        
                        case "--local":
                            Settings.isLocal = targetArgs[++i].Equals("true");
                            break;
                    }
                }
            } catch (IndexOutOfRangeException) {
                AlConsole.WriteLine(AlStatusEnum.Error, "ArgExcp", "Arg Parser", "Arguments is incorrect.");
            }
        }
    }
}