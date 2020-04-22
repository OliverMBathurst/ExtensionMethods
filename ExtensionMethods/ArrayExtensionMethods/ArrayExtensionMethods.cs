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
                array[array.Length - 1] = tmp;
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
                var tmp = array[array.Length - 1];
                for (var i = array.Length - 1; i > 0; i--)
                {
                    array[i] = array[i - 1];
                }
                array[0] = tmp;
            }
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
                arr[arr.Length - 1] = item;
            }

            return arr;
        }
    }
}
