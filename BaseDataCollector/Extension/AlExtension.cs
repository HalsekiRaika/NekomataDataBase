using System;
using Log5RLibs.Services;

namespace BaseDataCollector.Extension {
    public static class AlExtension {
        private static readonly object LockObj = new object();
        public static void ArrayWrite(AlCConfigScheme preset, string[] array) {
            foreach (string writeObj in array) {
                lock (LockObj) {
                    AlConsole.WriteLine(preset, writeObj);
                }
            }
        }

        public static void ColorizeWriteLine(AlCConfigScheme preset, string message, ConsoleColor[] colors, bool isAlConsole = true) {
            ColorizeWrite(preset, message, colors, isAlConsole);
            Console.WriteLine();
        }
        
        public static void ColorizeWrite(AlCConfigScheme preset, string message, ConsoleColor[] colors, bool isAlConsole = true) {
            if (isAlConsole) { AlConsole.Write(preset, ""); }
            string[] disParsedMessage = message.Split("^");
            for (int i = 0; i < disParsedMessage.Length; i++) {
                Console.ForegroundColor = colors[i];
                Console.Write(disParsedMessage[i]);
            }
            Console.ResetColor();
        }
    }
}