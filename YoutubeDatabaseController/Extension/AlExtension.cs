using Log5RLibs.Services;

namespace YoutubeDatabaseController.Extension {
    public class AlExtension {
        private static readonly object LockObj = new object();
        public static void ArrayWrite(AlCConfigScheme preset, string[] array) {
            foreach (string writeObj in array) {
                lock (LockObj) {
                    AlConsole.WriteLine(preset, writeObj);
                }
            }
        }
    }
}