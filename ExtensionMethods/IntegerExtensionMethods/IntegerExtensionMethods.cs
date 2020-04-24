using System;

namespace ExtensionMethods.IntegerExtensionMethods
{
    public static class IntegerExtensionMethods
    {
        public static bool IsInRange(this int current, int start, int end) => current >= start && end >= current;
        public static int[] To(this int initial, int to)
        {
            var arr = new int[Math.Abs(initial - to)];
            var index = 0;
            if (initial > to) {
                for (var i = initial; i > to; i--)
                {
                    arr[index] = i;
                    index++;
                }
            }
            else
            {
                for (var i = initial; i < to; i++)
                {
                    arr[index] = i;
                    index++;
                }
            }
            return arr;
        }
    }
}