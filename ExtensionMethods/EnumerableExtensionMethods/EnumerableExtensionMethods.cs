using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ExtensionMethods.Classes;
using ExtensionMethods.GenericExtensionMethods;
using ExtensionMethods.ListExtensionMethods;
using ExtensionMethods.ArrayExtensionMethods;


namespace ExtensionMethods.EnumerableExtensionMethods
{
    public static class EnumerableExtensionMethods
    {
        #region For extension methods
        public static void For<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var element in enumerable) action(element);
        }

        public static void For<T>(this IEnumerable<T> enumerable, Action<int> action)
        {
            for (var i = 0; i < enumerable.Count(); i++) action(i);
        }

        public static void For<T>(this IEnumerable<T> enumerable, Action<T, int> action)
        {
            var index = 0;
            foreach(var element in enumerable)
            {
                action(element, index);
                index++;
            }
        }

        public static void For<T>(this IEnumerable<T> enumerable, Action<IEnumerable<T>, int> action)
        {
            for (var i = 0; i < enumerable.Count(); i++) action(enumerable, i);
        }
        #endregion

        #region For and return extension methods
        public static IEnumerable<T> ForAndReturn<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            enumerable.For(action);
            return enumerable;
        }

        public static IEnumerable<T> ForAndReturn<T>(this IEnumerable<T> enumerable, Action<int> action)
        {
            enumerable.For(action);
            return enumerable;
        }

        public static IEnumerable<T> ForAndReturn<T>(this IEnumerable<T> enumerable, Action<T, int> action)
        {
            enumerable.For(action);
            return enumerable;
        }

        public static IEnumerable<T> ForAndReturn<T>(this IEnumerable<T> enumerable, Action<IEnumerable<T>, int> action)
        {
            enumerable.For(action);
            return enumerable;
        }
        #endregion

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

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) => enumerable == null || !enumerable.Any();

        public static T Random<T>(this IEnumerable<T> enumerable) => enumerable.ElementAt(new Random().Next(enumerable.Count() - 1));

        public static T RandomWhere<T>(this IEnumerable<T> enumerable, Func<T, bool> func) => enumerable.Where(func).Random();

        public static bool IsEmpty<T>(this IEnumerable<T> enumerable) => !enumerable.Any();

        public static IEnumerable<T> FillWith<T>(this IEnumerable<T> enumerable, T item) => enumerable.ToArray().ForAndReturn((e, i) => e[i] = item);

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

        public static IEnumerable<T> WhereNext<T>(this IEnumerable<T> enumerable, Func<T, bool> func) =>
            enumerable.Zip(enumerable.Skip(1), (previous, current) => new { previous, current })
                .Where(x => func(x.current))
                .Select(z => z.previous);

        public static object FirstOrNull<T>(this IEnumerable<T> enumerable, Func<T, bool> func) where T : class => enumerable.FirstOrDefault(func)?.Box();

        public static IEnumerable<T> AddN<T>(this IEnumerable<T> enumerable, int n)
        {
            var list = new List<T>(enumerable);
            for (var i = 0; i < n; i++)
                list.Add((T)Activator.CreateInstance(typeof(T)));
            return list;
        }

        public static IEnumerable<T> ReplaceAll<T>(this IEnumerable<T> enumerable, T itemToReplace, T replacementItem) where T : IComparable<T>
        =>
            enumerable.ToArray().ForAndReturn((array, index) =>
            {
                if (array[index].CompareTo(itemToReplace) == 0)
                    array[index] = replacementItem;
            });

        public static IEnumerable<T> Skip<T>(this IEnumerable<T> enumerable, params T[] args) => enumerable.Where(element => !args.Contains(element));

        public static IEnumerable<T> WithoutElementsAt<T>(this IEnumerable<T> enumerable, params int[] indexes)
        {
            var index = 0;
            foreach(var element in enumerable)
            {
                if (!indexes.Contains(index))
                    yield return element;

                index++;
            }
        }

        public static IEnumerable<T> WithoutElements<T>(this IEnumerable<T> enumerable, params T[] elements) => enumerable.Where(element => !elements.Contains(element));

        public static int IndexOf<T>(this IEnumerable<T> enumerable, T item) where T : IComparable<T>
        {
            var index = 0;
            foreach (var element in enumerable)
            {
                if (element.CompareTo(item) == 0)
                {
                    return index;
                }
                index++;
            }

            return -1;
        }

        public static bool Is<T>(this IEnumerable<T> enumerable, params T[] args) where T : IComparable<T>
        {
            if (enumerable.Count() != args.Length)
                return false;

            var iterable = new ConcurrentIterable<T, T>(enumerable.ToArray(), args).AsEnumerable();
            return iterable.All(tuple => tuple.Item1.CompareTo(tuple.Item2) == 0);
        }

        public static IEnumerable<T> Replace<T>(this IEnumerable<T> enumerable, T itemToReplace, T replacementItem) where T : IComparable<T>
        {
            var array = enumerable.ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                if (array[i].CompareTo(itemToReplace) != 0) continue;
                array[i] = replacementItem;
                return array;
            }
            return array;
        }

        public static IEnumerable<T> RemoveWhile<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate) => enumerable.Skip(enumerable.TakeWhile(predicate).Count());

        public static IEnumerable<T> RemoveAll<T>(this IEnumerable<T> enumerable, Func<T, bool> func) => enumerable.Where(element => !func(element));

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

        public static IEnumerable<T> ReplaceAll<T>(this IEnumerable<T> enumerable, Func<T, bool> func, T item) => enumerable.Select(element => func(element) ? item : element);

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
            enumerable.For((element) =>
            {
                funcs.For((function, functionIndex) =>
                {
                    if (function[functionIndex](element))
                    {
                        masterList[functionIndex].Add(element);
                    }
                });
            });

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
            return !result.SequenceEqual(enumerable) ? result : InternalShuffle(enumerable, r);
        }

        [ExcludeFromCodeCoverage]
        private static bool AllTheSame<T>(IEnumerable<T> enumerable) where T : IComparable<T>
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(IEnumerable<T>));

            if (enumerable.Count() < 2)
                return true;

            var comparison = enumerable.First();
            return enumerable.Skip(1).All(element => element.CompareTo(comparison) == 0);
        }
    }
}