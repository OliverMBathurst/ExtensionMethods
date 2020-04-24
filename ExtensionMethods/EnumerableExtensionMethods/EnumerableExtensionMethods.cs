﻿using ExtensionMethods.Exceptions;
using ExtensionMethods.GenericExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtensionMethods.EnumerableExtensionMethods
{
    public static class EnumerableExtensionMethods
    {
        public static T Get<T>(this IEnumerable<T> enumerable, int index) => enumerable.ElementAt(index);

        public static T Get<T>(this IEnumerable<T> enumerable, T item) where T : IComparable<T> => enumerable.FirstOrDefault(x => x.CompareTo(item) == 0);

        public static IEnumerable<T> GetAll<T>(this IEnumerable<T> enumerable, T item) where T : IComparable<T> => enumerable.Where(x => x.CompareTo(item) == 0);

        public static IEnumerable<T> Remove<T>(this IEnumerable<T> enumerable, T item) => enumerable.WithoutElements(item);

        public static IEnumerable<T> Remove<T>(this IEnumerable<T> enumerable, int index) => enumerable.WithoutElementsAt(index);

        public static IEnumerable<T> RemoveAll<T>(this IEnumerable<T> enumerable, T item) where T : IComparable<T> => enumerable.Where(x => x.CompareTo(item) != 0);

        public static IEnumerable<T> Add<T>(this IEnumerable<T> enumerable, T item) => enumerable.ChainableAdd(item);

        public static IEnumerable<T> BetweenValues<T>(this IEnumerable<T> enumerable, T lowerValue, T upperValue) where T : IComparable<T> => enumerable.Where(x => x.CompareTo(upperValue) < 0 && x.CompareTo(lowerValue) > 0);

        public static IEnumerable<T> BetweenValuesInclusive<T>(this IEnumerable<T> enumerable, T lowerValue, T upperValue) where T : IComparable<T> => enumerable.Where(x => x.CompareTo(upperValue) <= 0 && x.CompareTo(lowerValue) >= 0);

        public static bool AreAllTheSame<T>(this IEnumerable<T> enumerable) where T: IComparable<T> => AllTheSame(enumerable);

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) => enumerable == null || enumerable.Count() == 0 ? true : false;

        public static T Random<T>(this IEnumerable<T> enumerable) => enumerable.ElementAt(new Random().Next(enumerable.Count() - 1));

        public static T RandomWhere<T>(this IEnumerable<T> enumerable, Func<T, bool> func) => enumerable.Where(x => func(x)).Random();

        public static bool IsEmpty<T>(this IEnumerable<T> enumerable) => enumerable.Count() == 0;

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var e in enumerable) action(e);
        }

        public static void Shuffle<T>(this IEnumerable<T> enumerable)
        {
            var r = new Random();
            enumerable = enumerable.OrderBy(t => r.Next()).ToList();
        }

        public static bool IsDistinct<T>(this IEnumerable<T> enumerable)
        {
            var hashSet = new HashSet<T>();
            return enumerable.All(hashSet.Add);
        }

        public static void AddN<T>(this IEnumerable<T> enumerable, int n)
        {
            for (var i = 0; i < n; i++)
                enumerable = enumerable.ChainableAdd((T)Activator.CreateInstance(typeof(T)));
        }

        public static IEnumerable<T> WherePrevious<T>(this IEnumerable<T> enumerable, Func<T, bool> func) =>
            enumerable.Zip(enumerable.Skip(1), (previous, current) => new { previous, current })
                .Where(x => func(x.previous))
                .Select(z => z.current);

        public static object FirstOrNull<T>(this IEnumerable<T> enumerable, Func<T, bool> func) where T : class
        {
            if (!typeof(T).IsNullable()) throw new NotNullableException();

            var firstOrDefault = enumerable.FirstOrDefault(func);
            return firstOrDefault == default ? null : firstOrDefault.Box();
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

        public static bool All<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            var enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (!predicate(enumerator.Current))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Any<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            var enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (predicate(enumerator.Current))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool Is<T>(this IEnumerable<T> enumerable, params T[] args) where T : IComparable<T>
        {
            var comparisonArray = enumerable.ToArray();
            for (var i = 0; i < comparisonArray.Length; i++)
            {
                if (comparisonArray[i].CompareTo(args[i]) != 0)
                {
                    return false;
                }
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

        public static IEnumerable<IList<T>> Split<T>(this IEnumerable<T> enumerable, int n)
        {
            var masterList = new List<List<T>>();
            for(var i = 0; i < enumerable.Count(); i += n)
                masterList.Add(enumerable.Skip(i).Take(n).ToList());

            return masterList;
        }

        public static IEnumerable<IList<T>> MultiSplit<T>(this IEnumerable<T> enumerable, params Func<T, bool>[] funcs)
        {
            var masterList = new List<List<T>>();
            for (var i = 0; i < funcs.Length; i++)
                masterList.Add(new List<T>());

            var enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                for(var i = 0; i < funcs.Length; i++)
                {
                    if (funcs[i](enumerator.Current))
                    {
                        masterList[i].Add(enumerator.Current);
                    }
                }
            }

            return masterList;
        }

        private static IEnumerable<T> ChainableAdd<T>(this IEnumerable<T> enumerable, T item)
        {
            var list = enumerable.ToList();
            list.Add(item);
            return list;
        }

        private static bool AllTheSame<T>(IEnumerable<T> enumerable) where T : IComparable<T>
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(T));

            if (enumerable.Count() < 2)
                return true;

            var comparison = enumerable.First();
            for (var i = 1; i < enumerable.Count(); i++)
                if (enumerable.Get(i).CompareTo(comparison) != 0)
                    return false;
            return true;
        }
    }
}