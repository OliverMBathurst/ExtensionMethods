﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
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

        public static bool IsNullable<T>(this Type type)
        {
            if (type == null) return true;
            if (Nullable.GetUnderlyingType(type) != null || !type.IsValueType)
            {
                return true;
            }
            return false;
        }

        public static bool IsNull<T>(this T obj) => obj == null;

        public static bool IsIn<T>(this T obj, ICollection<T> collection) => collection.Contains(obj);

        public static bool IsIn<T>(this T obj, params T[] array) => array.Contains(obj);

        public static bool IsIn<T>(this T obj, IEnumerable<T> enumerable) => enumerable.Contains(obj);

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
            var stream = new MemoryStream();
            var bf = new BinaryFormatter();
            bf.Serialize(stream, obj);
            stream.Position = 0;
            var result = (T)bf.Deserialize(stream);
            stream.Close();
            return result;
        }

        public static C Parse<T, C>(this T obj) => (C)TypeDescriptor.GetConverter(typeof(C)).ConvertFrom(obj);

        public static void TryDispose<T>(this T obj, bool throwException = false)
        {
            if (!(obj is IDisposable disposable)) return;
            try { disposable.Dispose(); } catch (Exception e) { if (throwException) throw e; }
        }

        public static object NullIfDefault<T>(this T obj) where T: class => obj == default ? (object)null : obj;

        public static bool IsDefault<T>(this T obj) where T: class => obj == default;

        public static object Box<T>(this T obj) => (object)obj;

        public static T Unbox<T>(this object obj) => (T)obj;
    }
}