using System.Collections.Generic;
using System.Linq;

#pragma warning disable 0649
namespace Core.Scripts.Tools.Extensions
{
    public static class DictionaryExtensions
    {
        public static List<V> GetList<K, V>(this Dictionary<K, List<V>> dict, K key)
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, new List<V>());
            }

            return dict[key];
        }

        public static void AddToList<K, V>(this Dictionary<K, List<V>> dict, K key, V value)
        {
            dict.GetList(key).Add(value);
        }

        public static void Set<K, V>(this Dictionary<K, V> dict, K key, V value)
        {
            if (!dict.ContainsKey(key)) dict.Add(key, value);
            else dict[key] = value;
        }

        public static void RemoveAllByValue<K, V>(this Dictionary<K, V> dictionary, V value)
        {
            var keys = dictionary.Where(kvp => EqualityComparer<V>.Default.Equals(kvp.Value, value))
                .Select(x => x.Key).ToArray();
            foreach (var key in keys)
            {
                dictionary.Remove(key);
            }
        }

        public static bool IsNull<K, V>(this KeyValuePair<K, V> pair)
        {
            return pair.Equals(new KeyValuePair<K, V>());
        }
    }
}