using System;

namespace ExtensionMethods.RandomExtensionMethods
{
    public static class RandomExtensionMethods
    {
        public static T RandomElementOf<T>(this Random random, params T[] args) => args[random.Next(args.Length - 1)];
    }
}