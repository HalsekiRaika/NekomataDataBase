using System;
using Log5RLibs.Services;
using Log5RLibs.utils;

namespace RavenLauncher {
    public static class ArgParser {
        public static void Decomposition(string[] targetArgs) {
            try {
                for (int i = 0; i < targetArgs.Length; i++) {
                    switch (targetArgs[i]) {
                        case "--user":
                            Settings.DataBaseUserName = targetArgs[++i];
                            break;
                        
                        case "--pass":
                            Settings.DataBasePassWord = targetArgs[++i];
                            break;
                        
                        case "--local":
                            Settings.DataBaseIsLocal = targetArgs[++i].Equals("true");
                            break;
                        
                        case "--ismaintenance":
                            Settings.IsMaintenanceMode = true;
                            break;
                        
                        case "--githubusername":
                            Settings.GithubUserName = targetArgs[++i];
                            break;
                        
                        case "--githubpassword":
                            Settings.GithubPassWord = targetArgs[++i];
                            break;
                        
                        case "--githubtoken":
                            Settings.GithubToken = targetArgs[++i];
                            break;
                    }
                }
            } catch (IndexOutOfRangeException) {
                AlConsole.WriteLine(AlStatusEnum.Error, "ArgExcp", "Arg Parser", "Arguments is incorrect.");
            }
        }
    }
}