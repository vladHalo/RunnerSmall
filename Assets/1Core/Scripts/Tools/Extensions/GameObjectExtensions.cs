using UnityEngine;

#pragma warning disable 0649
namespace Core.Scripts.Tools.Extensions
{
    public static class GameObjectExtensions
    {
        public static bool IsDestroyed(this GameObject gameObject)
        {
            return gameObject == null && !ReferenceEquals(gameObject, null);
        }

        public static void DestroyGameObject(this GameObject go)
        {
            if (Application.isPlaying)
            {
                Object.Destroy(go);
            }
            else
            {
#if UNITY_EDITOR
                UnityEditor.Undo.DestroyObjectImmediate(go);
#endif
                Object.DestroyImmediate(go);
            }
        }

        public static void DestroyGameObject(this Component component)
        {
            component.gameObject.DestroyGameObject();
        }

        public static void DestroyComponent(this Component component)
        {
            if (Application.isPlaying)
            {
                Object.Destroy(component);
            }
            else
            {
                Object.DestroyImmediate(component);
            }
        }

        public static void SetLayerRecursively(this GameObject gameObject, int newLayer)
        {
            gameObject.layer = newLayer;

            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                SetLayerRecursively(gameObject.transform.GetChild(i).gameObject, newLayer);
            }
        }
    }
}