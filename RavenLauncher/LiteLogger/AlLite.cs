using System;

namespace RavenLauncher.LiteLogger {
    public static class AlLite {
        public static void WriteLine(WriteMode mode, string msg) {
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
            Console.WriteLine($" ] {DateTime.Now:MMddhhmmss}: " + msg);
        }
    }

    public enum WriteMode {
        INFO,
        CAUT,
        WARN,
        ERR,
    }
}