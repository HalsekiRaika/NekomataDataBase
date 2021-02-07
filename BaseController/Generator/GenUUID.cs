using System;

namespace BaseController.Generator {
    public class GenUuid {
        public static string Generate() {
            Guid guidValue = Guid.NewGuid();
            return guidValue.ToString();
        }
    }
}