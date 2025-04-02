using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
namespace Core.Scripts.Tools.Extensions
{
    public static class QuaternionExtensions
    {
        public static Quaternion GetLookAtRotation(this Transform self, Vector3 target)
        {
            return Quaternion.LookRotation(target - self.position);
        }

        public static Quaternion GetLookAtRotation(this Transform self, Transform target)
        {
            return GetLookAtRotation(self, target.position);
        }

        public static Quaternion GetLookAtRotation(this Transform self, GameObject target)
        {
            return GetLookAtRotation(self, target.transform.position);
        }

        public static void LookAwayFrom(this Transform self, Vector3 target)
        {
            self.rotation = GetLookAwayFromRotation(self, target);
        }

        public static void LookAwayFrom(this Transform self, Transform target)
        {
            self.rotation = GetLookAwayFromRotation(self, target);
        }

        public static void LookAwayFrom(this Transform self, GameObject target)
        {
            self.rotation = GetLookAwayFromRotation(self, target);
        }

        public static Quaternion GetLookAwayFromRotation(this Transform self, Vector3 target)
        {
            return Quaternion.LookRotation(self.position - target);
        }

        public static Quaternion GetLookAwayFromRotation(this Transform self, Transform target)
        {
            return GetLookAwayFromRotation(self, target.position);
        }

        public static Quaternion GetLookAwayFromRotation(this Transform self, GameObject target)
        {
            return GetLookAwayFromRotation(self, target.transform.position);
        }
    }
}