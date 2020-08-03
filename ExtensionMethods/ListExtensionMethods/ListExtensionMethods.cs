using System;
using System.Collections.Generic;

namespace ExtensionMethods.ListExtensionMethods
{
    public static class ListExtensionMethods
    {
        #region For extension methods
        public static void For<T>(this IList<T> list, Action<T> action)
        {
            foreach (var t in list)
                action(t);
        }

        public static void For<T>(this IList<T> list, Action<int> action)
        {
            for (var i = 0; i < list.Count; i++) action(i);
        }

        public static void For<T>(this IList<T> list, Action<T, int> action)
        {
            for (var i = 0; i < list.Count; i++) action(list[i], i);
        }

        public static void For<T>(this IList<T> list, Action<IList<T>, int> action)
        {
            for (var i = 0; i < list.Count; i++) action(list, i);
        }

        public static IList<T> ForAndReturn<T>(this IList<T> list, Action<T> action)
        {
            list.For(action);
            return list;
        }

        public static IList<T> ForAndReturn<T>(this IList<T> list, Action<int> action)
        {
            list.For(action);
            return list;
        }

        public static IList<T> ForAndReturn<T>(this IList<T> list, Action<T, int> action)
        {
            list.For(action);
            return list;
        }

        public static IList<T> ForAndReturn<T>(this IList<T> list, Action<IList<T>, int> action)
        {
            list.For(action);
            return list;
        }
        #endregion

        public static void Fill<T>(this IList<T> list, T item)
        {
            for (var i = 0; i < list.Count; i++)
                list[i] = item;
        }

        public static IList<T> ChainableFill<T>(this IList<T> list, T item)
        {
            for (var i = 0; i < list.Count; i++)
                list[i] = item;
            return list;
        }

        public static IList<T> ChainableListAdd<T>(this IList<T> list, T item)
        {
            list.Add(item);
            return list;
        }

        public static void AddN<T>(this IList<T> list, int n)
        {
            for (var i = 0; i < n; i++)
                list.Add((T)Activator.CreateInstance(typeof(T)));
        }

        public static void AddN<T, TS>(this IList<T> list, int n) where TS : T
        {
            for (var i = 0; i < n; i++)
                list.Add((TS)Activator.CreateInstance(typeof(TS)));
        }

        public static IList<T> ChainableAddN<T>(this IList<T> list, int n)
        {
            AddN(list, n);
            return list;
        }

        public static IList<T> ChainableAddN<T, TS>(this IList<T> list, int n) where TS : T
        {
            AddN<T, TS>(list, n);
            return list;
        }

        public static void InsertSorted<T>(this IList<T> list, T item) where T : IComparable<T>
        {
            if (item == null)
                throw new ArgumentNullException(nameof(T));
            if (list == null)
                throw new ArgumentNullException(nameof(IList<T>));

            for (var i = 0; i < list.Count; i++)
            {
                if (item.CompareTo(list[i]) >= 0) continue;
                list.Insert(i, item);
                return;
            }
            
            list.Add(item);
        }

        public static void InsertSorted<T>(this IList<T> list, T item, Comparison<T> comparison) where T : IComparable<T>
        {
            if (item == null)
                throw new ArgumentNullException(nameof(T));
            if (list == null)
                throw new ArgumentNullException(nameof(IList<T>));

            for (var i = 0; i < list.Count; i++)
            {
                if (comparison(item, list[i]) >= 0) continue;
                list.Insert(i, item);
                return;
            }
            
            list.Add(item);            
        }

        public static void InsertSorted<T>(this IList<T> list, T item, IComparer<T> comparer) where T : IComparable<T>
        {
            if (item == null)
                throw new ArgumentNullException(nameof(T));
            if (list == null)
                throw new ArgumentNullException(nameof(IList<T>));

            for (var i = 0; i < list.Count; i++)
            {
                if (comparer.Compare(item, list[i]) >= 0) continue;
                list.Insert(i, item);
                return;
            }
            
            list.Add(item);            
        }

        public static IList<T> InsertWhere<T>(this IList<T> list, T item, Func<T, bool> predicate, bool multipleInserts = false)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(T));
            if (list == null)
                throw new ArgumentNullException(nameof(IList<T>));

            var @return = new List<T>();
            for(var i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    @return.Add(item);
                    if (!multipleInserts)
                    {
                        for(var j = i; j < list.Count; j++)
                            @return.Add(list[j]);
                        return @return;
                    }
                }

                @return.Add(list[i]);
            }
            return @return;
        }
    }
}