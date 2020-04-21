using System;

namespace ExtensionMethods.BooleanExtensionMethods
{
    public static class BooleanExtensionMethods
    {
        public static bool Is<T>(this T value, T value1) where T : IComparable<T> => value.CompareTo(value1) == 0;

        public static bool Or(this bool value, bool value1) => value || value1;

        public static bool And(this bool value, bool value1) => value && value1;
    }
}
