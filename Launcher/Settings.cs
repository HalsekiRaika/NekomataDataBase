using System;
using System.IO;

namespace Launcher {
    public static class Settings {
        // Target Directories
        public static readonly string BaseDir   = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string CloneDir  = BaseDir + "Temps/";
        public static readonly string BuildDir  = BaseDir + "Controller/";
        public static readonly string StatusDir = BaseDir + "Stats/";
        public static readonly string ConfigDir = BuildDir + "Config/";

        // Program Argument
        public static bool DoSkipConfigGenerate { get; set; } = false;
        public static string DataBaseUserName  { get; set; }
        public static string DataBasePassWord  { get; set; }
        public static bool   DataBaseIsLocal   { get; set; } = false;

        public static string IgnoreDataArray { get; set; }

        // App Directories
        public static readonly string Controller = BuildDir + "YoutubeDatabaseController";
        
        // Stat Files
        public const string RecoveryStat = "recovery.stat";
        public const string DownloadedStat = "downloaded.stat";

        // Directory Info
        public static readonly DirectoryInfo StatInfo = new DirectoryInfo(StatusDir);
        public static readonly FileInfo RecvFile = new FileInfo(StatusDir + RecoveryStat);
        public static readonly FileInfo DlFile   = new FileInfo(StatusDir + DownloadedStat);
    }
}