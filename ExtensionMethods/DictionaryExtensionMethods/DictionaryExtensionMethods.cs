using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ExtensionMethods.DictionaryExtensionMethods
{
    public static class DictionaryExtensionMethods
    {
        public static IReadOnlyDictionary<T, C> AsReadOnly<T, C>(this IDictionary<T, C> dictionary) => new ReadOnlyDictionary<T, C>(dictionary);
    }
}