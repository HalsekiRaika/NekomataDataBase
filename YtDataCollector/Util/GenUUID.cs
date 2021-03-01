using System;

namespace YtDataCollector.Util {
    public static class GenUuid {
        public static string Generate() {
            Guid guidValue = Guid.NewGuid();
            return guidValue.ToString();
        }
    }
}