using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

#pragma warning disable 0649
namespace Core.Scripts.Tools.Extensions
{
    public static class ArrayExtensions
    {
        public static T Random<T>(this List<T> self)
        {
            if (self.Count == 0) return default;
            return self[UnityEngine.Random.Range(0, self.Count)];
        }

        public static T Random<T>(this List<T> self, Func<T, bool> filter)
        {
            if (self.Count == 0) return default;
            return self.Where(filter).ToList().Random();
        }

        public static int RandomIndex<T>(this List<T> self)
        {
            if (self.Count == 0) return default;
            return UnityEngine.Random.Range(0, self.Count);
        }

        public static T Middle<T>(this List<T> self)
        {
            if (self.Count == 0) return default;
            return self[Mathf.RoundToInt((float)self.Count / 2)];
        }

        public static T Random<T>(this T[] self)
        {
            if (self.Length == 0) return default;
            return self[UnityEngine.Random.Range(0, self.Length)];
        }

        public static T MinItem<T>(this IEnumerable<T> self, Func<T, float> selector, Func<T, bool> isValid = null)
        {
            var min = float.MaxValue;
            T selected = default;
            foreach (var item in self)
            {
                if (isValid != null && !isValid(item)) continue;
                var v = selector(item);
                if (v < min)
                {
                    min = v;
                    selected = item;
                }
            }

            return selected;
        }

        public static T MaxItem<T>(this IEnumerable<T> self, Func<T, float> selector, Func<T, bool> isValid = null)
        {
            var max = float.MinValue;
            T selected = default;
            foreach (var item in self)
            {
                if (isValid != null && !isValid(item)) continue;
                var v = selector(item);
                if (v > max)
                {
                    max = v;
                    selected = item;
                }
            }

            return selected;
        }

        public static void RemoveBy<T>(this List<T> self, Func<T, bool> condition)
        {
            for (var i = self.Count - 1; i >= 0; i--)
            {
                var item = self[i];
                if (condition(item)) self.Remove(item);
            }
        }

        public static void ForEach<T>(this T[] self, Action<T> action)
        {
            foreach (var item in self) action.Invoke(item);
        }

        public static void ForEach<T>(this IList<T> self, Action<T, int> action, bool reverse = false)
        {
            if (!reverse)
            {
                for (var i = 0; i < self.Count; i++) action(self[i], i);
            }
            else
            {
                for (var i = self.Count - 1; i >= 0; i--) action(self[i], i);
            }
        }

        public static List<T> ReverseNew<T>(this List<T> self)
        {
            var result = new List<T>();
            for (var i = self.Count - 1; i >= 0; i--)
            {
                var item = self[i];
                result.Add(item);
            }

            return result;
        }

        public static List<T> ToList<T>(this IEnumerable<T> source, List<T> target)
        {
            target.Clear();
            target.AddRange(source);
            return target;
        }
        
        public static void Shuffle<T>(this IList<T> list)
        {
            Random random = new Random();
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}