using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ExtensionMethods.DictionaryExtensionMethods
{
    public static class DictionaryExtensionMethods
    {
        public static IReadOnlyDictionary<K, V> AsReadOnly<K, V>(this IDictionary<K, V> dictionary) => new ReadOnlyDictionary<K, V>(dictionary);

        public static SortedDictionary<K, V> AsSorted<K, V>(this IDictionary<K, V> dictionary) => new SortedDictionary<K, V>(dictionary);

        public static SortedDictionary<K, V> AsSorted<K, V>(this IDictionary<K, V> dictionary, IComparer<K> comparer) => new SortedDictionary<K, V>(dictionary, comparer);

        public static bool Contains<K, V>(this IDictionary<K, V> dictionary, (K Key, V Value) keyValueTuple)
            => dictionary.Contains(new KeyValuePair<K, V>(keyValueTuple.Key, keyValueTuple.Value));

        public static void Add<K, V>(this IDictionary<K, V> dictionary, (K Key, V Value) keyValueTuple)
        {
            dictionary.Add(new KeyValuePair<K, V>(keyValueTuple.Key, keyValueTuple.Value));
        }

        public static void Remove<K, V>(this IDictionary<K, V> dictionary, (K Key, V Value) keyValueTuple)
        {
            dictionary.Remove(new KeyValuePair<K, V>(keyValueTuple.Key, keyValueTuple.Value));
        }

        public static void AddAll<K, V, T>(this IDictionary<K, V> dictionary, IEnumerable<T> enumerable, Func<T, K> keySelector, Func<T, V> valueSelector)
        {
            foreach(var element in enumerable)
                dictionary.Add((keySelector(element), valueSelector(element)));
        }

        public static IEnumerable<T> ToList<K, V, T>(this IDictionary<K, V> dictionary, Func<KeyValuePair<K, V>, T> func)
        {
            var list = new List<T>();
            foreach (var kvp in dictionary)
            {
                list.Add(func(kvp));
            }
            return list;
        }

        public static bool TryAdd<K, V>(this IDictionary<K, V> dictionary, (K Key, V Value) valueTuple)
        {
            if (dictionary.ContainsKey(valueTuple.Key))
                return false;

            dictionary.Add(valueTuple);
            return true;
        }

        public static bool TryRemove<K, V>(this IDictionary<K, V> dictionary, (K Key, V Value) valueTuple)
        {
            if (!dictionary.Contains(valueTuple))
                return false;

            dictionary.Remove(valueTuple);
            return true;
        }

        public static bool ContainsAll<K, V>(this IDictionary<K, V> dictionary, params (K Key, V Value)[] args)
        {
            foreach(var paramElement in args)
            {
                if (!dictionary.Contains(paramElement))
                    return false;
            }
            return true;
        }

        public static bool ContainsAny<K, V>(this IDictionary<K, V> dictionary, params (K Key, V Value)[] args)
        {
            foreach(var paramElement in args)
            {
                if (dictionary.Contains(paramElement))
                    return true;
            }
            return false;
        }

        public static bool ContainsAnyKey<K, V>(this IDictionary<K, V> dictionary, params K[] keys)
        {
            foreach (var key in keys)
            {
                if (dictionary.ContainsKey(key))
                    return true;
            }
            return false;
        }

        public static bool ContainsAllKeys<K, V>(this IDictionary<K, V> dictionary, params K[] keys)
        {
            foreach (var key in keys)
            {
                if (!dictionary.ContainsKey(key))
                    return false;
            }
            return true;
        }

        public static bool AddOrReturn<K, V>(this IDictionary<K, V> dictionary, (K Key, V Value) keyValueTuple, out V value)
        {
            var success = false;
            if (!dictionary.ContainsKey(keyValueTuple.Key))
            {
                dictionary.Add(keyValueTuple);
                success = true;
            }
            value = dictionary[keyValueTuple.Key];
            return success;
        }

        public static bool Is<K, V>(this IDictionary<K, V> dictionary, params (K Key, V Value)[] args) 
            where K : IComparable<K> 
            where V : IComparable<V>
        {
            if (dictionary.Count != args.Length)
                return false;

            var index = 0;
            foreach(var kvp in dictionary)
            {
                var (Key, Value) = args[index];
                if(kvp.Key.CompareTo(Key) != 0 || kvp.Value.CompareTo(Value) != 0)
                    return false;
                index++;
            }
            return true;
        }
    }
}