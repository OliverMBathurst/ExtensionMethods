using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ExtensionMethods.GenericExtensionMethods
{
    public static class GenericExtensionMethods
    {
        public static void IfType<T>(this object obj, Action<T> action) where T : class
        {
            if(obj is T t)
            {
                action(t);
            }
        }

        public static void IfType<T>(this object obj, Action action) where T : class
        {
            if(obj is T)
            {
                action();
            }
        }

        public static T IfNullThen<T>(this T original, T replacement)
        {
            return original ?? replacement;
        }

        public static void IfNotNull<T>(this T obj, Action<T> action)
        {
            if(obj != null)
            {
                action(obj);
            }
        }

        public static bool IsNullable<T>(this T obj)
        {
            if (obj == null) return true;
            var type = typeof(T);
            if (Nullable.GetUnderlyingType(type) != null || !type.IsValueType)
            {
                return true;
            }
            return false;
        }

        public static bool IsNull<T>(this T obj) => obj == null;

        public static bool IsIn<T>(this T obj, ICollection<T> collection) => collection.Contains(obj);

        public static void ThrowIf<T>(this T obj, Func<T, bool> func)
        {
            if (func(obj))
            {
                throw new Exception();
            }
        }

        public static void ThrowIf<T>(this T obj, Func<T, bool> func, Exception e)
        {
            if (func(obj))
            {
                throw e;
            }
        }

        public static void ThrowIfEquals<T>(this T obj, T value) where T : IComparable<T>
        {
            if(obj.CompareTo(value) == 0)
            {
                throw new Exception();
            }
        }

        public static void ThrowIfEquals<T>(this T obj, T value, Exception e) where T : IComparable<T>
        {
            if(obj.CompareTo(value) == 0)
            {
                throw e;
            }
        }

        public static T DeepClone<T>(this T obj)
        {
            if(obj == null || !obj.GetType().IsSerializable)
            {
                return default;
            }
            var bf = new BinaryFormatter();
            using var stream = new MemoryStream();
            bf.Serialize(stream, obj);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)bf.Deserialize(stream);
        }
    }
}
