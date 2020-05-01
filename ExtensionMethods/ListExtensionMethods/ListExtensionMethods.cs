using System;
using System.Collections.Generic;

namespace ExtensionMethods.ListExtensionMethods
{
    public static class ListExtensionMethods
    {
        //for and forandreturn methods here
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

        public static IList<T> ChainableAddN<T>(this IList<T> list, int n)
        {
            AddN(list, n);
            return list;
        }

        public static void InsertSorted<T>(this IList<T> list, T item) where T : IComparable<T>
        {
            if (item == null || list == null)
                throw new ArgumentNullException(nameof(T));

            for (var i = 0; i < list.Count; i++)
            {
                if (item.CompareTo(list[i]) < 0)
                {
                    list.Insert(i, item);
                    return;
                }
            }
            
            list.Add(item);
        }

        public static void InsertSorted<T>(this IList<T> list, T item, Comparison<T> comparison) where T : IComparable<T>
        {
            if (item == null || list == null)
                throw new ArgumentNullException(nameof(T));

            for (var i = 0; i < list.Count; i++)
            {
                if (comparison(item, list[i]) < 0)
                {
                    list.Insert(i, item);
                    return;
                }
            }
            
            list.Add(item);            
        }

        public static void InsertSorted<T>(this IList<T> list, T item, IComparer<T> comparer) where T : IComparable<T>
        {
            if (item == null || list == null)
                throw new ArgumentNullException(nameof(T));

            for (var i = 0; i < list.Count; i++)
            {
                if (comparer.Compare(item, list[i]) < 0)
                {
                    list.Insert(i, item);
                    return;
                }
            }
            
            list.Add(item);            
        }

        public static IList<T> InsertWhere<T>(this IList<T> list, T item, Func<T, bool> predicate, bool multipleInserts = false)
        {
            if (item == null || list == null)
                throw new ArgumentNullException(nameof(T));

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