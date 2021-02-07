using System;
using YoutubeDatabaseController;
using YoutubeDatabaseController.Extension;
using YoutubeDatabaseController.Scheme.LogScheme;

namespace TwitterDataBaseController {
    public static class ControlMain {
        static void Main(string[] args) {
            AlExtension.ArrayWrite(DefaultScheme.START_UP, Settings.Startup);
            
        }
    }
}