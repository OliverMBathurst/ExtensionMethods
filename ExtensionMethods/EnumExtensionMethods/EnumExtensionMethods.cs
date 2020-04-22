using System;
using System.Collections.Generic;

namespace ExtensionMethods.EnumExtensionMethods
{
    public static class EnumExtensionMethods
    {
        public static IDictionary<int, T> ToDictionary<T>() where T : struct
        {
            var dict = new Dictionary<int, T>();
            if (!typeof(T).IsEnum)
                return dict;

            var index = 0;
            foreach (var enumValue in Enum.GetValues(typeof(T)))
            {
                dict.Add(index, (T)enumValue);
                index++;
            }

            return dict;
        }
    }
}