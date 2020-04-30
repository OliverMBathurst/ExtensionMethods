using ExtensionMethods.Classes;
using System;
using System.Collections.Generic;

namespace ExtensionMethods.EnumExtensionMethods
{
    public static class EnumExtensionMethods
    {
        public static IDictionary<string, T> ToDictionary<T>() where T : struct
        {
            var dict = new Dictionary<string, T>();
            if (!typeof(T).IsEnum)
                return dict;

            foreach (var result in new ConcurrentIterable<string, T>(Enum.GetNames(typeof(T)), (T[])Enum.GetValues(typeof(T))).AsEnumerable())
                dict.Add(result.Item1, result.Item2);

            return dict;
        }
    }
}