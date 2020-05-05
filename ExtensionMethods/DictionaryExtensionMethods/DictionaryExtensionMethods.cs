using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ExtensionMethods.DictionaryExtensionMethods
{
    public static class DictionaryExtensionMethods
    {
        public static bool Contains<K, V>(this IDictionary<K, V> dictionary, (K Key, V Value) keyValueTuple)
            => dictionary.Contains(new KeyValuePair<K, V>(keyValueTuple.Key, keyValueTuple.Value));

        public static IReadOnlyDictionary<K, V> AsReadOnly<K, V>(this IDictionary<K, V> dictionary) => new ReadOnlyDictionary<K, V>(dictionary);
    
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