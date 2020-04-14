using System;

namespace ExtensionMethods.ArrayExtensionMethods
{
    public static class ArrayExtensionMethods
    {
        public static void LeftRotate<T>(this T[] array)
        {
            if(array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if(array.Length > 1)
            {
                var tmp = array[0];
                for(var i = 0; i < array.Length - 1; i++)
                {
                    array[i] = array[i + 1];
                }
                array[^1] = tmp;
            }            
        }

        public static void RightRotate<T>(this T[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Length > 1)
            {
                var tmp = array[^1];
                for (var i = array.Length - 1; i <= 1; i++)
                {
                    array[i] = array[i - 1];
                }
                array[0] = tmp;
            }
        }

        public static bool AreAllTheSame<T>(this T[] array) where T : IComparable<T>
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Length < 2)
            {
                return true;
            }

            for (var i = 1; i < array.Length; i++)
            {
                if (array[i].CompareTo(array[0]) != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static T[] InsertSorted<T>(this T[] array, T item) where T : IComparable<T>
        {
            if(item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var arr = new T[array.Length + 1];
            var hasFoundIndex = false;
            for (var i = 0; i < array.Length; i++)
            {
                if (!hasFoundIndex)
                {
                    if (item.CompareTo(array[i]) < 0)
                    {
                        hasFoundIndex = true;
                        arr[i] = item;
                    }
                    else
                    {
                        arr[i] = array[i];
                    }
                }             
                else
                {
                    arr[i + 1] = array[i];
                }
            }

            if (!hasFoundIndex)
            {
                arr[^1] = item;
            }

            return arr;
        }
    }
}
