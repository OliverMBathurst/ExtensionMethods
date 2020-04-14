using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtensionMethods.EnumerableExtensionMethods
{
    public static class CollectionExtensionMethods
    {
        public static bool All<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            var enumerator = collection.GetEnumerator();
            while(enumerator.MoveNext())
            {
                if (!predicate(enumerator.Current))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Any<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            var enumerator = collection.GetEnumerator();
            while(enumerator.MoveNext())
            {
                if (predicate(enumerator.Current))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            if (collection == null || collection.Count == 0)
            {
                return true;
            }
            return false;
        }

        public static void Shuffle<T>(this ICollection<T> collection)
        {
            var r = new Random();
            collection = collection.OrderBy(t => r.Next()).ToList();
        }

        public static bool IsUnique<T>(this ICollection<T> collection)
        {
            var hashSet = new HashSet<T>();
            return collection.All(hashSet.Add);
        }

        public static ICollection<T> Add<T>(this ICollection<T> collection, T item)
        {
            collection.Add(item);
            return collection;
        }

        public static ICollection<T> BetweenValues<T>(this ICollection<T> collection, T lowerValue, T upperValue) where T : IComparable<T>
        {
            var list = new List<T>();
            var enumerator = collection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (current.CompareTo(upperValue) < 0 && current.CompareTo(lowerValue) > 0)
                {
                    list.Add(current);
                }
            }
            return list;
        }

        public static ICollection<T> BetweenValuesInclusive<T>(this ICollection<T> collection, T lowerValue, T upperValue) where T : IComparable<T>
        {
            var list = new List<T>();
            var enumerator = collection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (current.CompareTo(upperValue) <= 0 && current.CompareTo(lowerValue) >= 0)
                {
                    list.Add(current);
                }
            }
            return list;
        }

        public static void RemoveWhile<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            var enumerator = collection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (predicate(enumerator.Current))
                {
                    collection.Remove(enumerator.Current);
                }
                else
                {
                    break;
                }
            }
        }
    }
}
