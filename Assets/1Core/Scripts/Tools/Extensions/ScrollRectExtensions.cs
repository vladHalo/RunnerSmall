using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0649
namespace Core.Scripts.Tools.Extensions
{
    public static class ScrollRectExtensions
    {
        public static float NormalizeScrollDistance(this ScrollRect scrollRect, int axis, float distance)
        {
            var viewport = scrollRect.viewport;
            var viewRect = viewport != null ? viewport : scrollRect.GetComponent<RectTransform>();
            var viewBounds = new Bounds(viewRect.rect.center, viewRect.rect.size);

            var content = scrollRect.content;
            var contentBounds = content != null ? content.TransformBoundsTo(viewRect) : new Bounds();

            var hiddenLength = contentBounds.size[axis] - viewBounds.size[axis];
            return distance / hiddenLength;
        }
        
        public static void ScrollToCeneter(this ScrollRect scrollRect, RectTransform target)
        {
            var view = scrollRect.viewport != null ? scrollRect.viewport : scrollRect.GetComponent<RectTransform>();
            
            var viewRect = view.rect;
            var elementBounds = target.TransformBoundsTo(view);
            var offset = viewRect.center.y - elementBounds.center.y;
            
            var scrollPos = scrollRect.verticalNormalizedPosition - scrollRect.NormalizeScrollDistance(1, offset);
            scrollRect.verticalNormalizedPosition = Mathf.Clamp(scrollPos, 0f, 1f);
        }
    }
}