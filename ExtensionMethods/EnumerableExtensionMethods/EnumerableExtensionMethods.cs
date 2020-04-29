using ExtensionMethods.GenericExtensionMethods;
using ExtensionMethods.ListExtensionMethods;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ExtensionMethods.EnumerableExtensionMethods
{
    public static class EnumerableExtensionMethods
    {
        public static T Get<T>(this IEnumerable<T> enumerable, int index) => enumerable.ElementAt(index);

        public static T Get<T>(this IEnumerable<T> enumerable, T item) where T : IComparable<T> => enumerable.FirstOrDefault(x => x.CompareTo(item) == 0);

        public static IEnumerable<T> GetAll<T>(this IEnumerable<T> enumerable, T item) where T : IComparable<T> => enumerable.Where(x => x.CompareTo(item) == 0);

        public static IEnumerable<T> Remove<T>(this IEnumerable<T> enumerable, T item) => enumerable.WithoutElements(item);

        public static IEnumerable<T> RemoveAtIndex<T>(this IEnumerable<T> enumerable, int index) => enumerable.WithoutElementsAt(index);

        public static IEnumerable<T> RemoveAll<T>(this IEnumerable<T> enumerable, T item) where T : IComparable<T> => enumerable.Where(x => x.CompareTo(item) != 0);

        public static IEnumerable<T> Add<T>(this IEnumerable<T> enumerable, T item) => enumerable.ChainableAdd(item);

        public static IEnumerable<T> BetweenValues<T>(this IEnumerable<T> enumerable, T lowerValue, T upperValue) where T : IComparable<T> => enumerable.Where(x => x.CompareTo(upperValue) < 0 && x.CompareTo(lowerValue) > 0);

        public static IEnumerable<T> BetweenValuesInclusive<T>(this IEnumerable<T> enumerable, T lowerValue, T upperValue) where T : IComparable<T> => enumerable.Where(x => x.CompareTo(upperValue) <= 0 && x.CompareTo(lowerValue) >= 0);

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable) => InternalShuffle(enumerable, new Random());

        public static bool AreAllTheSame<T>(this IEnumerable<T> enumerable) where T: IComparable<T> => AllTheSame(enumerable);

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) => enumerable == null || enumerable.Count() == 0 ? true : false;

        public static T Random<T>(this IEnumerable<T> enumerable) => enumerable.ElementAt(new Random().Next(enumerable.Count() - 1));

        public static T RandomWhere<T>(this IEnumerable<T> enumerable, Func<T, bool> func) => enumerable.Where(x => func(x)).Random();

        public static bool IsEmpty<T>(this IEnumerable<T> enumerable) => enumerable.Count() == 0;

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var e in enumerable) action(e);
        }

        public static bool IsDistinct<T>(this IEnumerable<T> enumerable)
        {
            var hashSet = new HashSet<T>();
            return enumerable.All(hashSet.Add);
        }

        public static IEnumerable<T> WherePrevious<T>(this IEnumerable<T> enumerable, Func<T, bool> func) =>
            enumerable.Zip(enumerable.Skip(1), (previous, current) => new { previous, current })
                .Where(x => func(x.previous))
                .Select(z => z.current);

        public static object FirstOrNull<T>(this IEnumerable<T> enumerable, Func<T, bool> func) where T : class
        {
            var firstOrDefault = enumerable.FirstOrDefault(func);
            return firstOrDefault == default ? null : firstOrDefault.Box();
        }

        public static IEnumerable<T> AddN<T>(this IEnumerable<T> enumerable, int n)
        {
            var list = new List<T>(enumerable);
            for (var i = 0; i < n; i++)
                list.Add((T)Activator.CreateInstance(typeof(T)));
            return list;
        }

        public static IEnumerable<T> FillWith<T>(this IEnumerable<T> enumerable, T item)
        {
            var array = enumerable.ToArray();
            for (var i = 0; i < array.Length; i++)
                array[i] = item;
            return array;
        }

        public static IEnumerable<T> WithoutElementsAt<T>(this IEnumerable<T> enumerable, params int[] indexes)
        {
            for (var i = 0; i < enumerable.Count(); i++)
            {
                if (!indexes.Contains(i))
                {
                    yield return enumerable.ElementAt(i);
                }
            }
        }

        public static IEnumerable<T> WithoutElements<T>(this IEnumerable<T> enumerable, params T[] elements)
        {
            for (var i = 0; i < enumerable.Count(); i++)
            {
                if (!elements.Contains(enumerable.ElementAt(i)))
                {
                    yield return enumerable.ElementAt(i);
                }
            }
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

        public static bool Is<T>(this IEnumerable<T> enumerable, params T[] args) where T : IComparable<T>
        {
            if (enumerable.Count() != args.Length)
                return false;

            var index = 0;
            foreach(var element in enumerable)
            {
                if (element.CompareTo(args[index]) != 0)
                    return false;

                index++;
            }
            return true;
        }

        public static IEnumerable<T> Replace<T>(this IEnumerable<T> enumerable, T itemtoReplace, T replacementItem) where T : IComparable<T>
        {
            var list = enumerable.ToList();
            for (var i = 0; i < list.Count(); i++)
            {
                if (list[i].CompareTo(itemtoReplace) == 0)
                {
                    list[i] = replacementItem;
                    return list;
                }
            }
            return list;
        }

        public static IEnumerable<T> RemoveWhile<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            var count = 0;
            foreach(var element in enumerable)
            {
                if (!predicate(element))
                    break;

                count++;
            }
            return enumerable.Skip(count);
        }

        public static IEnumerable<T> RemoveAll<T>(this IEnumerable<T> enumerable, Func<T, bool> func)
        {
            foreach(var element in enumerable)
            {
                if (!func(element))
                {
                    yield return element;
                }
            }
        }

        public static IEnumerable<int> AllIndexesOf<T>(this IEnumerable<T> enumerable, T item) where T : IComparable<T>
        {
            var index = 0;
            foreach(var element in enumerable)
            {
                if (element.CompareTo(item) == 0)
                {
                    yield return index;
                }
                index++;
            }
        }

        public static IEnumerable<T> ReplaceAll<T>(this IEnumerable<T> enumerable, Func<T, bool> func, T item)
        {
            foreach(var element in enumerable)
            {
                yield return func(element) ? item : element;
            }
        }

        public static IEnumerable<IList<T>> Split<T>(this IEnumerable<T> enumerable, int n)
        {
            var masterList = new List<List<T>>();
            for(var i = 0; i < enumerable.Count(); i += n)
                masterList.Add(enumerable.Skip(i).Take(n).ToList());

            return masterList;
        }

        public static IEnumerable<IList<T>> MultiSplit<T>(this IEnumerable<T> enumerable, params Func<T, bool>[] funcs)
        {
            var masterList = new List<List<T>>().ChainableAddN(funcs.Length);
            foreach (var element in enumerable)
            {
                for(var i = 0; i < funcs.Length; i++)
                {
                    if (funcs[i](element))
                    {
                        masterList[i].Add(element);
                    }
                }
            }

            return masterList;
        }

        [ExcludeFromCodeCoverage]
        private static IEnumerable<T> ChainableAdd<T>(this IEnumerable<T> enumerable, T item) => enumerable.ToList().ChainableListAdd(item);

        [ExcludeFromCodeCoverage]
        private static IEnumerable<T> InternalShuffle<T>(IEnumerable<T> enumerable, Random r)
        {
            if (enumerable.Count() < 2)
                return enumerable;
            var result = enumerable.OrderBy(t => r.Next()).ToList();
            if (!result.SequenceEqual(enumerable))
                return result;
            else
                return InternalShuffle(enumerable, r);
        }

        [ExcludeFromCodeCoverage]
        private static bool AllTheSame<T>(IEnumerable<T> enumerable) where T : IComparable<T>
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(T));

            if (enumerable.Count() < 2)
                return true;

            var comparison = enumerable.First();
            foreach(var element in enumerable.Skip(1))
                if (element.CompareTo(comparison) != 0)
                    return false;
            return true;
        }
    }
}