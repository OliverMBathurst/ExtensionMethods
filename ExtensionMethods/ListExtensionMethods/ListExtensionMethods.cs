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
    }
}
