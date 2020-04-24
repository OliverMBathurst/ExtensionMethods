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

            var index = 0;

            var names = Enum.GetNames(typeof(T));
            foreach (var enumValue in Enum.GetValues(typeof(T)))
            {
                dict.Add(names[index], (T)enumValue);
                index++;
            }

            return dict;
        }
    }
}