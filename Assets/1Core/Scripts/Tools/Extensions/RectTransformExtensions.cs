using UnityEngine;

#pragma warning disable 0649
namespace Core.Scripts.Tools.Extensions
{
    public static class RectTransformExtensions
    {
        public static void SetSize(this RectTransform self, Vector2 size)
        {
            Vector2 oldSize = self.rect.size;
            Vector2 deltaSize = size - oldSize;

            self.offsetMin -= new Vector2(
                deltaSize.x * self.pivot.x,
                deltaSize.y * self.pivot.y);
            self.offsetMax += new Vector2(
                deltaSize.x * (1f - self.pivot.x),
                deltaSize.y * (1f - self.pivot.y));
        }

        public static void SetSize(this RectTransform self, float width, float height)
        {
            self.SetSize(new Vector2(width, height));
        }

        public static void SetSize(this RectTransform self, float size)
        {
            self.SetSize(new Vector2(size, size));
        }

        public static void SetWidth(this RectTransform self, float size)
        {
            self.SetSize(new Vector2(size, self.rect.size.y));
        }

        public static void SetHeight(this RectTransform self, float size)
        {
            self.SetSize(new Vector2(self.rect.size.x, size));
        }
        
        static Vector3[] corners = new Vector3[4];
        
        public static Bounds TransformBoundsTo(this RectTransform source, Transform target)
        {
            // Based on code in ScrollRect's internal GetBounds and InternalGetBounds methods
            var bounds = new Bounds();
            if (source != null)
            {
                source.GetWorldCorners(corners);

                var vMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
                var vMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);

                var matrix = target.worldToLocalMatrix;
                for (int j = 0; j < 4; j++)
                {
                    Vector3 v = matrix.MultiplyPoint3x4(corners[j]);
                    vMin = Vector3.Min(v, vMin);
                    vMax = Vector3.Max(v, vMax);
                }

                bounds = new Bounds(vMin, Vector3.zero);
                bounds.Encapsulate(vMax);
            }

            return bounds;
        }

        public static bool Contains(this RectTransform rt, Vector2 point)
        {
            Rect rect = rt.rect;
            
            float leftSide = rt.anchoredPosition.x - rect.width / 2;
            float rightSide = rt.anchoredPosition.x + rect.width / 2;
            float topSide = rt.anchoredPosition.y + rect.height / 2;
            float bottomSide = rt.anchoredPosition.y - rect.height / 2;

            //Debug.Log(leftSide + ", " + rightSide + ", " + topSide + ", " + bottomSide);
            
            return point.x >= leftSide &&
                   point.x <= rightSide &&
                   point.y >= bottomSide &&
                   point.y <= topSide;
        }
    }
}