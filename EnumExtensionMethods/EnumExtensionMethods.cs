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
            var attributes = typeof(TEnum).CustomAttributes;
            var enumerator = attributes.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if(enumerator.Current.AttributeType == typeof(DescriptionAttribute))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
