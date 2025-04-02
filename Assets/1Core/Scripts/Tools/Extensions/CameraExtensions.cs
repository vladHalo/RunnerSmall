using UnityEngine;

#pragma warning disable 0649
namespace Core.Scripts.Tools.Extensions
{
    public static class CameraExtensions
    {
        public static void PositionToBounds(this Camera camera, Bounds bounds, float marginPercentage = 1f)
        {
            var centerAtBack = new Vector3(bounds.center.x, bounds.center.y, bounds.min.z);
            var centerAtRightBack = new Vector3(bounds.max.x, bounds.center.y, bounds.min.z);
            var centerAtTopBack = new Vector3(bounds.center.x, bounds.max.y, bounds.min.z);
            var centerAtFrontBack = new Vector3(bounds.center.x, bounds.center.y, bounds.center.z);
            var centerToRightDist = (centerAtRightBack - centerAtBack).magnitude;
            var centerToTopDist = (centerAtTopBack - centerAtBack).magnitude;
            var centerToFrontDist = (centerAtFrontBack - centerAtBack).magnitude;
            var maxCenterTo = Mathfs.Max(centerToRightDist, centerToTopDist, centerToFrontDist);
            var minDistance = (maxCenterTo * marginPercentage) /
                              Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);

            camera.transform.localPosition =
                camera.transform.forward * -minDistance +
                new Vector3(bounds.center.x,
                    bounds.center.y); //new Vector3(bounds.center.x, bounds.center.y, -minDistance);
        }
    }
}