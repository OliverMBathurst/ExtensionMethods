using System.Collections.Generic;
using System.Linq;

namespace ExtensionMethods.EnumerableExtensionMethods
{
    public static class EnumerableExtensionMethods
    {
        public static bool IsDistinct<T>(this IEnumerable<T> enumerable)
        {
            var hashSet = new HashSet<T>();
            return enumerable.All(hashSet.Add);
        }
    }
}
