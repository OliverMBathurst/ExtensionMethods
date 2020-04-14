using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtensionMethods.StringExtensionMethods
{
    public static class StringExtensionMethods
    {
        public static bool IsUnique(this string str)
        {
            var hashSet = new HashSet<char>();
            return str.All(hashSet.Add);
        }

        public static string Substring(this string str, int startIndex, int endIndex) => str.Substring(startIndex, endIndex - startIndex);
        
        public static long? ToLong(this string str)
        {
            var parseResult = long.TryParse(str, out long result);
            return parseResult ? result : new long?();
        }

        public static string DefaultIfEmpty(this string str, string defaultValue) => string.IsNullOrWhiteSpace(str) ? defaultValue : str;
    }
}
