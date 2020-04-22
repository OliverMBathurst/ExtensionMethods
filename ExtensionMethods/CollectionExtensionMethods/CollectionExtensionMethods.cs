using ExtensionMethods.GenericExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtensionMethods.EnumerableExtensionMethods
{
    public static class CollectionExtensionMethods
    {
        public static ICollection<T> WherePrevious<T>(this ICollection<T> collection, Func<T, bool> func) =>
            collection.Zip(collection.Skip(1), (previous, current) => new { previous, current })
                .Where(x => func(x.previous))
                .Select(z => z.current)
                .ToCollection();

        public static object FirstOrNull<T>(this ICollection<T> collection, Func<T, bool> func) where T: class
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

        public static bool IsEmpty<T>(this ICollection<T> collection) => collection.Count == 0;

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

        public static ICollection<T> ChainableRemoveAt<T>(this ICollection<T> collection, int index)
        {
            collection.Remove(index);
            return collection;
        }

        public static ICollection<T> ChainableRemove<T>(this ICollection<T> collection, T item)
        {
            collection.Remove(item);
            return collection;
        }

        public static IEnumerable<T> BetweenValues<T>(this ICollection<T> collection, T lowerValue, T upperValue) where T : IComparable<T> => collection.Where(x => x.CompareTo(upperValue) < 0 && x.CompareTo(lowerValue) > 0);

        public static IEnumerable<T> BetweenValuesInclusive<T>(this ICollection<T> collection, T lowerValue, T upperValue) where T : IComparable<T> => collection.Where(x => x.CompareTo(upperValue) <= 0 && x.CompareTo(lowerValue) >= 0);

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

        public static T Get<T>(this ICollection<T> collection, int index) => collection.ElementAt(index);

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

        public static T Random<T>(this ICollection<T> collection) => collection.ElementAt(new Random().Next(collection.Count() - 1));
    }
}
