using System;
using LibGit2Sharp;
using SetupLibs.Logger;

namespace Launcher {
    public static class LibraryInstaller {
        public static void onInstall() {
            if (!Settings.IsLibraryInstall) { return; }
            try {
                AlLite.WriteLine(WriteMode.INFO, "Downloading NekomataLibrary...", false);
                Repository.Clone(Settings.LibraryUrl, Settings.ConfigDir);
                Console.WriteLine("Finished!");
            } catch (Exception e) {
                Console.WriteLine("Failure...");
                Console.WriteLine(e);
                throw;
            }
        }
    }
}