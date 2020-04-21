using ExtensionMethods.GenericExtensionMethods;
using ExtensionMethods.ListExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtensionMethods.EnumerableExtensionMethods
{
    public static class EnumerableExtensionMethods
    {
        public static IEnumerable<T> WherePrevious<T>(this IEnumerable<T> enumerable, Func<T, bool> func) =>
            enumerable.Zip(enumerable.Skip(1), (previous, current) => new { previous, current })
                .Where(x => func(x.previous))
                .Select(z => z.current);

        public static object FirstOrNull<T>(this IEnumerable<T> enumerable, Func<T, bool> func)
        {
            if (!typeof(T).IsNullable()) throw new NotNullableException();

            var firstOrDefault = enumerable.FirstOrDefault(func);
            return firstOrDefault == default ? null : firstOrDefault.Box();
        }

        public static bool IsDistinct<T>(this IEnumerable<T> enumerable)
        {
            var hashSet = new HashSet<T>();
            return enumerable.All(hashSet.Add);
        }

        public static IEnumerable<T> Replace<T>(this IEnumerable<T> enumerable, T itemtoReplace, T replacementItem) where T : IComparable<T>
        {
            var list = enumerable.ToList();
            for(var i = 0; i < list.Count(); i++)
            {
                if(list[i].CompareTo(itemtoReplace) == 0)
                {
                    list[i] = replacementItem;
                    return list;
                }
            }
            return list;
        }

        public static IEnumerable<T> ReplaceAll<T>(this IEnumerable<T> enumerable, T itemtoReplace, T replacementItem) where T : IComparable<T>
        {
            var list = enumerable.ToList();
            for (var i = 0; i < list.Count(); i++)
            {
                if (list[i].CompareTo(itemtoReplace) == 0)
                {
                    list[i] = replacementItem;
                }
            }
            return list;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) => enumerable == null || enumerable.Count() == 0 ? true : false;

        public static void Shuffle<T>(this IEnumerable<T> enumerable)
        {
            var r = new Random();
            enumerable = enumerable.OrderBy(t => r.Next()).ToList();
        }

        public static ICollection<T> ToCollection<T>(this IEnumerable<T> enumerable) => (ICollection<T>)enumerable;

        public static IEnumerable<T> ChainableAdd<T>(this IEnumerable<T> enumerable, T item) => enumerable.ToList().ChainableAdd(item);

        public static IEnumerable<T> BetweenValues<T>(this IEnumerable<T> enumerable, T lowerValue, T upperValue) where T : IComparable<T>
        {
            return enumerable.Where(x => x.CompareTo(upperValue) < 0 && x.CompareTo(lowerValue) > 0);
        }

        public static IEnumerable<T> BetweenValuesInclusive<T>(this IEnumerable<T> enumerable, T lowerValue, T upperValue) where T : IComparable<T>
        {
            return enumerable.Where(x => x.CompareTo(upperValue) <= 0 && x.CompareTo(lowerValue) >= 0);
        }

        public static IEnumerable<T> RemoveWhile<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            var count = 0;
            var enumerator = enumerable.GetEnumerator();            
            while (enumerator.MoveNext())
            {
                if (!predicate(enumerator.Current))                
                {
                    break;
                }
                count++;
            }
            return enumerable.Skip(count);
        }

        public static IEnumerable<int> AllIndexesOf<T>(this IEnumerable<T> enumerable, T item) where T : IComparable<T>
        {
            var indexes = new List<int>();
            var enumerator = enumerable.GetEnumerator();
            var index = 0;
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.CompareTo(item) == 0)
                {
                    indexes.Add(index);
                }
                index++;
            }

            return indexes;
        }

        public static T Get<T>(this IEnumerable<T> enumerable, int index) => enumerable.ElementAt(index);

        public static T Get<T>(this IEnumerable<T> enumerable, T item) where T : IComparable<T>
        {
            return enumerable.FirstOrDefault(x => x.CompareTo(item) == 0);
        }

        public static IEnumerable<T> GetAll<T>(this IEnumerable<T> enumerable, T item) where T : IComparable<T>
        {
            return enumerable.Where(x => x.CompareTo(item) == 0);
        }

        public static IEnumerable<T> Remove<T>(this IEnumerable<T> enumerable, T item) where T : IComparable<T>
        {
            return enumerable.ToList().ChainableRemove(item);
        }

        public static IEnumerable<T> Remove<T>(this IEnumerable<T> enumerable, int index) => enumerable.ToList().ChainableRemoveAt(index);

        public static IEnumerable<T> RemoveAll<T>(this IEnumerable<T> enumerable, T item) where T : IComparable<T>
        {
            return enumerable.Where(x => x.CompareTo(item) != 0);
        }

        public static IEnumerable<T> RemoveAll<T>(this IEnumerable<T> enumerable, Func<T, bool> func)
        {
            var list = new List<T>();
            var enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (!func(enumerator.Current))
                {
                    list.Add(enumerator.Current);
                }
            }
            return list;
        }

        public static IEnumerable<T> ReplaceAll<T>(this IEnumerable<T> enumerable, Func<T, bool> func, T item)
        {
            var list = new List<T>();
            var enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (func(enumerator.Current))
                {
                    list.Add(item);
                }
                else
                {
                    list.Add(enumerator.Current);
                }
            }
            return list;
        }

        public static T Random<T>(this IEnumerable<T> enumerable) => enumerable.ElementAt(new Random().Next(enumerable.Count() - 1));
    }
}
