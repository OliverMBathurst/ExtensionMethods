﻿using System;
using System.Text;

namespace ExtensionMethods.StringExtensionMethods
{
    public static class StringExtensionMethods
    {
        public static string SubString(this string str, int startIndex, int endIndex) => str.SubString(startIndex, endIndex - startIndex);
        
        public static long? ToLong(this string str) => long.TryParse(str, out long result) ? result : new long?();

        public static string DefaultIfEmpty(this string str, string defaultValue) => string.IsNullOrWhiteSpace(str) ? defaultValue : str;

        public static string Repeat(this string str, int n)
        {
            var strBuilder = new StringBuilder(str.Length * n);
            for(var i = 0; i < n; i++)
            {
                strBuilder.Append(str);
            }
            return strBuilder.ToString();
        }

        public static Exception ToException(this string str) => new Exception(str);
    }
}
