using System;

namespace SetupLibs.Logger {
    public static class AlLite {
        public static void WriteLine(WriteMode mode, string msg, bool isNewline = true) {
            Console.Write("[ ");
            switch (mode) {
                case WriteMode.INFO:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("INFO");
                    Console.ResetColor();
                    break;
                
                case WriteMode.CAUT:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("CAUT");
                    Console.ResetColor();
                    break;
                
                case WriteMode.WARN:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("WARN");
                    Console.ResetColor();
                    break;
                
                case WriteMode.ERR:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("ERR*");
                    Console.ResetColor();
                    break;
            }

            if (isNewline) {
                Console.WriteLine($" ] {DateTime.Now:MMddhhmmss}: " + msg);
            } else {
                Console.Write($" ] {DateTime.Now:MMddhhmmss}: " + msg);              
            }
        }
    }

    public enum WriteMode {
        INFO,
        CAUT,
        WARN,
        ERR,
    }
}