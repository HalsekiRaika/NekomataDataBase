namespace SetupProcessor {
    public static class Encrypter {
        public static string onEncrypt(string target) {
            return target.Replace(target, string.Empty.PadRight(target.Length, '*'));
        }
    }
}