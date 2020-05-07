namespace ExtensionMethods.CharExtensionMethods
{
    public static class CharExtensionMethods
    {
        public static bool IsDigit(this char c) => char.IsDigit(c);

        public static bool IsLetter(this char c) => char.IsLetter(c);

        public static bool IsLetterOrDigit(this char c) => char.IsLetterOrDigit(c);

        public static bool IsLower(this char c) => char.IsLower(c);

        public static bool IsNumber(this char c) => char.IsNumber(c);

        public static bool IsUpper(this char c) => char.IsUpper(c);

        public static bool IsWhiteSpace(this char c) => char.IsWhiteSpace(c);

        public static string Repeat(this char c, int repeatCount) => new string(c, repeatCount);

        public static char ToLower(this char c) => char.ToLower(c);

        public static char ToUpper(this char c) => char.ToUpper(c);
    }
}