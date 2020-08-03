using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ExtensionMethods.DictionaryExtensionMethods
{
    public static class DictionaryExtensionMethods
    {
        public static IReadOnlyDictionary<TK, TV> AsReadOnly<TK, TV>(this IDictionary<TK, TV> dictionary) => new ReadOnlyDictionary<TK, TV>(dictionary);

        public static SortedDictionary<TK, TV> AsSorted<TK, TV>(this IDictionary<TK, TV> dictionary) => new SortedDictionary<TK, TV>(dictionary);

        public static SortedDictionary<TK, TV> AsSorted<TK, TV>(this IDictionary<TK, TV> dictionary, IComparer<TK> comparer) => new SortedDictionary<TK, TV>(dictionary, comparer);

        public static bool Contains<TK, TV>(this IDictionary<TK, TV> dictionary, (TK Key, TV Value) keyValueTuple)
            => dictionary.Contains(new KeyValuePair<TK, TV>(keyValueTuple.Key, keyValueTuple.Value));

        public static void Add<TK, TV>(this IDictionary<TK, TV> dictionary, (TK Key, TV Value) keyValueTuple)
            => dictionary.Add(new KeyValuePair<TK, TV>(keyValueTuple.Key, keyValueTuple.Value));

        public static void Remove<TK, TV>(this IDictionary<TK, TV> dictionary, (TK Key, TV Value) keyValueTuple) => dictionary.Remove(new KeyValuePair<TK, TV>(keyValueTuple.Key, keyValueTuple.Value));

        public static void AddAll<TK, TV, T>(this IDictionary<TK, TV> dictionary, IEnumerable<T> enumerable, Func<T, TK> keySelector, Func<T, TV> valueSelector)
        {
            foreach(var element in enumerable)
                dictionary.Add((keySelector(element), valueSelector(element)));
        }

        public static IEnumerable<T> ToList<TK, TV, T>(this IDictionary<TK, TV> dictionary, Func<KeyValuePair<TK, TV>, T> func) => dictionary.Select(func);

        public static bool TryAdd<TK, TV>(this IDictionary<TK, TV> dictionary, (TK Key, TV Value) valueTuple)
        {
            if (dictionary.ContainsKey(valueTuple.Key))
                return false;

            dictionary.Add(valueTuple);
            return true;
        }

        public static bool TryRemove<TK, TV>(this IDictionary<TK, TV> dictionary, (TK Key, TV Value) valueTuple)
        {
            if (!dictionary.Contains(valueTuple))
                return false;

            dictionary.Remove(valueTuple);
            return true;
        }

        public static bool ContainsAll<TK, TV>(this IDictionary<TK, TV> dictionary, params (TK Key, TV Value)[] args) => args.All(dictionary.Contains);

        public static bool ContainsAny<TK, TV>(this IDictionary<TK, TV> dictionary, params (TK Key, TV Value)[] args) => args.Any(dictionary.Contains);

        public static bool ContainsAnyKey<TK, TV>(this IDictionary<TK, TV> dictionary, params TK[] keys) => keys.Any(dictionary.ContainsKey);

        public static bool ContainsAllKeys<TK, TV>(this IDictionary<TK, TV> dictionary, params TK[] keys) => keys.All(dictionary.ContainsKey);

        public static bool AddOrReturn<TK, TV>(this IDictionary<TK, TV> dictionary, (TK Key, TV Value) keyValueTuple, out TV value)
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

        public static bool Is<TK, TV>(this IDictionary<TK, TV> dictionary, params (TK Key, TV Value)[] args) 
            where TK : IComparable<TK> 
            where TV : IComparable<TV>
        {
            if (dictionary.Count != args.Length)
                return false;

            var index = 0;
            foreach(var kvp in dictionary)
            {
                var (key, value) = args[index];
                if(kvp.Key.CompareTo(key) != 0 || kvp.Value.CompareTo(value) != 0)
                    return false;
                index++;
            }
            return true;
        }
    }
}