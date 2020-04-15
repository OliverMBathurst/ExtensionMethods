namespace ExtensionMethods.StringExtensionMethods
{
    public static class StringExtensionMethods
    {
        public static string SubString(this string str, int startIndex, int endIndex) => str[startIndex..endIndex];
        
        public static long? ToLong(this string str) => long.TryParse(str, out long result) ? result : new long?();

        public static string DefaultIfEmpty(this string str, string defaultValue) => string.IsNullOrWhiteSpace(str) ? defaultValue : str;
    }
}
