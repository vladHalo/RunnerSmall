using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

#pragma warning disable 0649
namespace Core.Scripts.Tools.Extensions
{
    public static class TransformExtensions
    {
        public static void Reset(this Transform transform)
        {
            transform.ResetPosition();
            transform.ResetRotation();
            transform.ResetScale();
        }

        public static void ResetPosition(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
        }

        public static void ResetRotation(this Transform transform)
        {
            transform.localEulerAngles = Vector3.zero;
        }

        public static void ResetScale(this Transform transform)
        {
            transform.localScale = Vector3.one;
        }

        public static void DestroyAllChildren(this Transform transform, int startFrom = 0)
        {
            for (int i = transform.childCount - 1; i >= startFrom; i--)
            {
                var child = transform.GetChild(i);
                child.gameObject.DestroyGameObject();
            }
        }

        [PublicAPI]
        public static void DestroyAllChildren(this Transform transform, params GameObject[] except)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                var child = transform.GetChild(i);
                if (except.Contains(child.gameObject)) continue;
                child.gameObject.DestroyGameObject();
            }
        }

        [PublicAPI]
        public static void DestroyAllChildren(this Transform transform, GameObject except)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                var child = transform.GetChild(i);
                if (except == child.gameObject) continue;
                child.gameObject.DestroyGameObject();
            }
        }

        [PublicAPI]
        public static void DestroyAllChildren<T>(this Transform transform, params T[] except) where T : Component
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                var child = transform.GetChild(i);
                if (child.TryGetComponent(typeof(T), out Component c) && !except.Contains(c))
                {
                    child.DestroyGameObject();
                }
            }
        }

        public static void MoveAllChildren(this Transform transform, Transform newParent)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                var child = transform.GetChild(i);
                child.SetParent(newParent);
            }
        }

        public static Bounds CalculateBounds(this Transform transform, params Renderer[] except)
        {
            return transform.CalculateBounds(false, except);
        }

        public static Bounds CalculateBounds(this Transform transform, bool ignoreParticleSystems,
            params Renderer[] except)
        {
            var renderers = transform.GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0) return default;

            Vector3 currentPosition = transform.position;
            Quaternion currentRotation = transform.rotation;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;

            var first = renderers.FirstOrDefault(r => !except.Contains(r));
            if (first == null) return default;
            Bounds bounds = first.bounds;
            for (int i = 1; i < renderers.Length; i++)
            {
                var r = renderers[i];
                if (ignoreParticleSystems && r is ParticleSystemRenderer) continue;
                if (except != null && except.Contains(r)) continue;
                bounds.Encapsulate(renderers[i].bounds);
            }

            // Vector3 localCenter = bounds.center - transform.position;
            // bounds.center = localCenter;
            transform.position = currentPosition;
            transform.rotation = currentRotation;
            return bounds;
        }

        public static Bounds CalculateBoundsWithTransforms(this Transform transform)
        {
            Bounds bounds = new Bounds(transform.position, Vector3.zero);
            Vector3 currentPosition = transform.position;
            Quaternion currentRotation = transform.rotation;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            foreach (Renderer renderer in transform.GetComponentsInChildren<Renderer>())
            {
                bounds.Encapsulate(renderer.bounds);
            }

            // bounds.center -= transform.position;
            transform.position = currentPosition;
            transform.rotation = currentRotation;
            return bounds;
        }

        public static Bounds CalculateBounds(List<Transform> transforms)
        {
            Bounds bounds = new Bounds();
            transforms.ForEach(transform =>
            {
                Quaternion currentRotation = transform.rotation;
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                foreach (Renderer renderer in transform.GetComponentsInChildren<Renderer>())
                {
                    bounds.Encapsulate(renderer.bounds);
                }

                Vector3 localCenter = bounds.center - transform.position;
                bounds.center = localCenter;
                transform.rotation = currentRotation;
            });

            return bounds;
        }

        public static void PivotTo(this Transform transform, Vector3 position)
        {
            Vector3 offset = transform.position - position;
            foreach (Transform child in transform)
                child.transform.position += offset;
            transform.position = position;
        }

        public static void MovePivotToBoundsCenter(this Transform transform)
        {
            var b = transform.CalculateBounds();
            PivotTo(transform, transform.position + b.center);
        }

        public static void BillboardView(this Transform transform, Camera _camera)
        {
            var rotation = _camera.transform.rotation;
            transform.LookAt(transform.position + rotation * Vector3.forward,
                rotation * Vector3.up);
        }

        public static void ScaleAroundRelative(this Transform target, Vector3 pivot, Vector3 scaleFactor)
        {
            var pivotDelta = target.localPosition - pivot;
            pivotDelta.Scale(scaleFactor);
            target.localPosition = pivot + pivotDelta;

            var finalScale = target.localScale;
            finalScale.Scale(scaleFactor);
            target.localScale = finalScale;
        }

        public static void ScaleAround(this Transform target, Vector3 pivot, Vector3 newScale)
        {
            Vector3
                pivotDelta = target.transform.localPosition - pivot; // diff from object pivot to desired pivot/origin
            Vector3 scaleFactor = new Vector3(
                newScale.x / target.localScale.x,
                newScale.y / target.localScale.y,
                newScale.z / target.localScale.z);
            pivotDelta.Scale(scaleFactor);
            target.localPosition = pivot + pivotDelta;

            target.localScale = newScale;
        }

        public static IEnumerable<Transform> FindAll(this Transform transform, string name, bool contains = false)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (contains && child.name.Contains(name) || child.name == name) yield return child;
            }
        }

        public static void OnOneChild(this Transform parent, int indexChild)
        {
            foreach (Transform i in parent)
            {
                i.gameObject.SetActive(false);
            }

            parent.GetChild(indexChild).gameObject.SetActive(true);
        }
    }
}