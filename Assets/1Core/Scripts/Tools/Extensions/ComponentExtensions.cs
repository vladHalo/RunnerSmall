using UnityEngine;

#pragma warning disable 0649
namespace Core.Scripts.Tools.Extensions
{
    public static class ComponentExtensions
    {
        public static T GetOrAddComponent<T>(this Component component) where T : Component
        {
            if (component.TryGetComponent(out T t)) return t;

            return component.gameObject.AddComponent<T>();
        }

        public static void ForEachComponent<T>(this T[] array, System.Action<T> callback) where T : Component
        {
            for (var i = 0; i < array.Length; i++)
            {
                callback.Invoke(array[i]);
            }
        }
    }
}