using ExtensionMethods.EnumerableExtensionMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ExtensionMethods.EnumExtensionMethods
{
    public static class EnumExtensionMethods
    {
        public static IDictionary<int, TEnum> ToDictionary<TEnum>(this TEnum _) where TEnum : Enum
        {
            var dict = new Dictionary<int, TEnum>();
            var values = Enum.GetValues(typeof(TEnum));
            var enumerator = values.GetEnumerator();
            var index = 0;
            while (enumerator.MoveNext())
            {
                dict.Add(index, (TEnum)enumerator.Current);
                index++;
            }

            return dict;
        }

        public static bool HasDescription<TEnum>(this TEnum _) where TEnum : Enum
        {
            return typeof(TEnum).CustomAttributes.FirstOrNull(x => x.AttributeType == typeof(DescriptionAttribute)) != null;
        }
    }
}
