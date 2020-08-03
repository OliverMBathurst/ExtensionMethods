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

            foreach (var (item1, item2) in new ConcurrentIterable<string, T>(Enum.GetNames(typeof(T)), (T[])Enum.GetValues(typeof(T))).AsEnumerable())
                dict.Add(item1, item2);

            return dict;
        }
    }
}