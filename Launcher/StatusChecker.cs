using System;
using System.IO;
using SetupLibs.Logger;
using SetupLibs.Model;

namespace Launcher {
    public static class StatusChecker {
        public static bool IsConfigVerified(ConfigModel target) {
            bool isApiVerified    = target.API_KEY != SetupLibs.Settings.ApikeyTemplateMessage;
            bool isDbUserVerified = target.DB_ACCESS_USERNAME != SetupLibs.Settings.UserNameTemplateString;
            bool isDbPassVerified = target.DB_ACCESS_PASSWORD != SetupLibs.Settings.PassWordTemplateString;

            if (!isApiVerified) {
                AlLite.WriteLine(WriteMode.ERR, "APIKey is still in the template state and has not been changed.");
            } else if (!isDbUserVerified) {
                AlLite.WriteLine(WriteMode.ERR, "DbUserName is still in the template state and has not been changed.");
            } else if (!isDbPassVerified) {
                AlLite.WriteLine(WriteMode.ERR, "DbPassWord is still in the template state and has not been changed.");
            }
            return isApiVerified && isDbUserVerified && isDbPassVerified;
        }
        
        public static bool IsControllerBuilt() {
            if (Directory.Exists(Settings.BuildDir)) {
                if (File.Exists(Settings.BuildDir + "*")) {
                    AlLite.WriteLine(WriteMode.INFO, "Controller Ready!");
                    return true;
                } else {
                    AlLite.WriteLine(WriteMode.ERR, "Not found Controller...");
                    return false;
                }
            } else {
                AlLite.WriteLine(WriteMode.ERR, "Not found Controller Directory...");
                return false;
            }
        }
        
        public static void MakeStatusDir() {
            Settings.StatInfo.Create();
        }
        
        public static bool IsRecoveryMode() {
            return Settings.RecvFile.Exists;
        }
    }
}