using System;

namespace YoutubeDatabaseController.Util {
    public class GenUuid {
        public static string Generate() {
            Guid guidValue = Guid.NewGuid();
            return guidValue.ToString();
        }
    }
}