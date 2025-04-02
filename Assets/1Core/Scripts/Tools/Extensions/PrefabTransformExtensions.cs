using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR

#endif

#pragma warning disable 0649
namespace Core.Scripts.Tools.Extensions
{
    public static class PrefabTransformExtensions
    {
        public static bool RevertAllAddedChildren(this Transform transform)
        {
#if UNITY_EDITOR
            if (PrefabUtility.IsPartOfAnyPrefab(transform))
            {
                for (int i = transform.childCount - 1; i >= 0; i--)
                {
                    var child = transform.GetChild(i);

                    PrefabUtility.RevertAddedGameObject(child.gameObject, InteractionMode.AutomatedAction);
                }

                return true;
            }
#endif

            return false;
        }
    }
}