using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ExtensionMethods.DictionaryExtensionMethods
{
    public static class DictionaryExtensionMethods
    {
        public static IReadOnlyDictionary<T, C> AsReadOnly<T, C>(this IDictionary<T, C> dictionary) => new ReadOnlyDictionary<T, C>(dictionary);
    
        public static bool Is<K, V>(this IDictionary<K, V> dictionary, params (K Key, V Value)[] args) 
            where K : IComparable<K> 
            where V : IComparable<V>
        {
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