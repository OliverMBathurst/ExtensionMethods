using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace ExtensionMethods.GenericExtensionMethods
{
    public static class GenericExtensionMethods
    {
        public static bool IsNull<T>(this T obj) => obj == null;

        public static bool IsIn<T>(this T obj, params T[] array) => array.Contains(obj);

        public static bool IsIn<T>(this T obj, IEnumerable<T> enumerable) => enumerable.Contains(obj);

        public static C Parse<T, C>(this T obj) => (C)TypeDescriptor.GetConverter(typeof(C)).ConvertFrom(obj);

        public static object NullIfDefault<T>(this T obj) where T : class => obj == default ? null : obj;

        public static string ToMD5Hash<T>(this T obj) => BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(GetBytes(obj)));

        public static bool IsDefault<T>(this T obj) where T : class => obj == default;

        public static object Box<T>(this T obj) => (object)obj;

        public static T Unbox<T>(this object obj) => (T)obj;

        public static void TryDispose<T>(this T obj)
        {
            if (!(obj is IDisposable disposable)) return;
            disposable.Dispose();
        }

        public static void ThrowIf<T>(this T obj, Func<T, bool> func)
        {
            if (func(obj))
                throw new Exception();
        }

        public static void ThrowIf<T>(this T obj, Func<T, bool> func, Exception e)
        {
            if (func(obj))
                throw e;
        }

        public static void ThrowIfEquals<T>(this T obj, T value) where T : IComparable<T>
        {
            if(obj.CompareTo(value) == 0)
                throw new Exception();
        }

        public static void ThrowIfEquals<T>(this T obj, T value, Exception e) where T : IComparable<T>
        {
            if(obj.CompareTo(value) == 0)
                throw e;
        }

        public static void IfType<T>(this object obj, Action<T> action) where T : class
        {
            if (obj is T t)
                action(t);
        }

        public static void IfType<T>(this object obj, Action action) where T : class
        {
            if (obj is T)
                action();
        }

        public static void IfNotNull<T>(this T obj, Action<T> action)
        {
            if (obj != null)
                action(obj);
        }

        public static byte[] GetBytes<T>(this T obj)
        {
            if (obj.IsNull()) throw new ArgumentNullException(nameof(T));
            byte[] byteArray;
            using (var memoryStream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(memoryStream, obj);
                byteArray = memoryStream.ToArray();
            }
            return byteArray;
        }

        public static void Repeat<T>(this T obj, Action<T> action, int times)
        {
            if (obj.IsNull()) throw new ArgumentNullException(nameof(T));
            for(var i = 0; i < times; i++)
                action(obj);
        }

        public static bool IsNullable<T>(this T obj)
        {
            if (obj == null) return true;
            var type = typeof(T);
            if (Nullable.GetUnderlyingType(type) != null || !type.IsValueType)
                return true;

            return false;
        }

        public static T DeepClone<T>(this T obj)
        {
            if(obj == null || !obj.GetType().IsSerializable)
                return default;

            T result;
            using (var stream = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(stream, obj);
                stream.Position = 0;
                result = (T)bf.Deserialize(stream);
            }                
            return result;
        }
    }
}
