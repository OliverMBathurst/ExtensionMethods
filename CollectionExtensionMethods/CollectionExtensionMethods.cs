using ExtensionMethods.GenericExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtensionMethods.EnumerableExtensionMethods
{
    public static class CollectionExtensionMethods
    {
        public static object FirstOrNull<T>(this ICollection<T> collection, Func<T, bool> func)
        {
            if (!typeof(T).IsNullable()) throw new NotNullableException();

            var firstOrDefault = collection.FirstOrDefault(func);
            return firstOrDefault == default ? null : firstOrDefault.Box();
        }

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

        public static bool IsNullOrEmpty<T>(this ICollection<T> collection) => collection == null || collection.Count == 0 ? true : false;

        public static IEnumerable<T> Shuffle<T>(this ICollection<T> collection)
        {
            var r = new Random();
            return collection.OrderBy(t => r.Next());
        }

        public static bool IsDistinct<T>(this ICollection<T> collection)
        {
            var hashSet = new HashSet<T>();
            return collection.All(hashSet.Add);
        }

        public static ICollection<T> ChainableAdd<T>(this ICollection<T> collection, T item)
        {
            collection.Add(item);
            return collection;
        }

        public static IEnumerable<T> BetweenValues<T>(this ICollection<T> collection, T lowerValue, T upperValue) where T : IComparable<T>
        {
            return collection.Where(x => x.CompareTo(upperValue) < 0 && x.CompareTo(lowerValue) > 0);
        }

        public static IEnumerable<T> BetweenValuesInclusive<T>(this ICollection<T> collection, T lowerValue, T upperValue) where T : IComparable<T>
        {
            return collection.Where(x => x.CompareTo(upperValue) <= 0 && x.CompareTo(lowerValue) >= 0);
        }

        public static IEnumerable<T> RemoveWhile<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            var count = 0;
            var enumerator = collection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (!predicate(enumerator.Current))
                {
                    break;
                }
                count++;
            }
            return collection.Skip(count);
        }

        public static ICollection<int> AllIndexesOf<T>(this ICollection<T> collection, T item) where T : IComparable<T>
        {
            var indexes = new List<int>();
            var enumerator = collection.GetEnumerator();
            var index = 0;
            while (enumerator.MoveNext())
            {
                if(enumerator.Current.CompareTo(item) == 0)
                {
                    indexes.Add(index);
                }
                index++;
            }

            return indexes;
        }

        public static T Get<T>(this ICollection<T> collection, int index)
        {
            var i = 0;
            var enumerator = collection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if(i == index)
                {
                    return enumerator.Current;
                }
                i++;
            }
            return default;
        }

        public static T Get<T>(this ICollection<T> collection, T item) where T : IComparable<T>
        {
            var enumerator = collection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if(enumerator.Current.CompareTo(item) == 0)
                {
                    return enumerator.Current;
                }
            }
            return default;
        }

        public static bool Remove<T>(this ICollection<T> collection, int index) => collection.Remove(Get(collection, index));
    }
}
