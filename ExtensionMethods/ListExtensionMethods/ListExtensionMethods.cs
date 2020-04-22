using System;
using System.Collections.Generic;

namespace ExtensionMethods.ListExtensionMethods
{
    public static class ListExtensionMethods
    {
        public static void InsertSorted<T>(this IList<T> list, T item) where T : IComparable<T>
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (list.Count > 0)
            {
                for (var i = 0; i < list.Count; i++)
                {
                    if (item.CompareTo(list[i]) < 0)
                    {
                        list.Insert(i, item);
                        return;
                    }
                }
            }
            list.Add(item);
        }

        public static void InsertSorted<T>(this IList<T> list, T item, Comparison<T> comparison) where T : IComparable<T>
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (list.Count > 0)
            {
                for (var i = 0; i < list.Count; i++)
                {
                    if (comparison(item, list[i]) < 0)
                    {
                        list.Insert(i, item);
                        return;
                    }
                }
            }
            list.Add(item);            
        }

        public static void InsertSorted<T>(this IList<T> list, T item, IComparer<T> comparer) where T : IComparable<T>
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (list.Count > 0)
            {
                for (var i = 0; i < list.Count; i++)
                {
                    if (comparer.Compare(item, list[i]) < 0)
                    {
                        list.Insert(i, item);
                        return;
                    }
                }
            }
            list.Add(item);            
        }

        public static bool AreAllTheSame<T>(this IList<T> list) where T : IComparable<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (list.Count < 2)
            {
                return true;
            }

            for (var i = 1; i < list.Count; i++)
            {
                if (list[i].CompareTo(list[0]) != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static IList<T> InsertWhere<T>(this IList<T> list, T item, Func<T, bool> predicate, bool multipleInserts = false)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

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
