﻿using System;

namespace ExtensionMethods.IntegerExtensionMethods
{
    public static class IntegerExtensionMethods
    {
        public static bool IsInRange(this int current, int start, int end) => current >= start && end >= current;

        public static void ForTo(this int current, int to, Action<int> action)
        {
            if (current > to)
                for (var i = current; i > to; i--)
                    action(i);
            else if (to > current)
                for (var i = current; i < to; i++)
                    action(i);
        }

        public static void ForFromZero(this int current, Action<int> action)
        {
            var ascending = !(current < 0);
            for(var i = 0; i < Math.Abs(current); i++)
                action(ascending ? i : -i);
        }

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