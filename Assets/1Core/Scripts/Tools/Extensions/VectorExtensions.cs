using UnityEngine;

#pragma warning disable 0649
namespace Core.Scripts.Tools.Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 SetX(this Vector2 v, float x) => new Vector2(x, v.y);
        public static Vector2 SetY(this Vector2 v, float y) => new Vector2(v.x, y);
        public static Vector3 SetX(this Vector3 v, float x) => new Vector3(x, v.y, v.z);
        public static Vector3 SetY(this Vector3 v, float y) => new Vector3(v.x, y, v.z);
        public static Vector3 SetZ(this Vector3 v, float z) => new Vector3(v.x, v.y, z);

        public static Vector2 AddX(this Vector2 v, float x) => new Vector2(v.x + x, v.y);
        public static Vector2 AddY(this Vector2 v, float y) => new Vector2(v.x, v.y + y);

        public static Vector3 AddX(this Vector3 v, float x) => new Vector3(v.x + x, v.y, v.z);
        public static Vector3 AddY(this Vector3 v, float y) => new Vector3(v.x, v.y + y, v.z);
        public static Vector3 AddZ(this Vector3 v, float z) => new Vector3(v.x, v.y, v.z + z);

        public static float RandomXY(this Vector2 v)
        {
            return UnityEngine.Random.Range(v.x, v.y);
        }

        public static int RandomXY(this Vector2Int v)
        {
            return UnityEngine.Random.Range(v.x, v.y);
        }

        public static float LerpXY(this Vector2 v, float t) => Mathf.Lerp(v.x, v.y, t);

        public static float RemapInXY(this Vector2 v, float oMin, float oMax, float value)
        {
            return Mathfs.Remap(v.x, v.y, oMin, oMax, value);
        }

        public static float RemapOutXY(this Vector2 v, float iMin, float iMax, float value)
        {
            return Mathfs.Remap(iMin, iMax, v.x, v.y, value);
        }

        public static Vector2 Abs(this Vector2 v)
        {
            v.x = Mathf.Abs(v.x);
            v.y = Mathf.Abs(v.y);
            return v;
        }

        public static Vector3 Abs(this Vector3 v)
        {
            v.x = Mathf.Abs(v.x);
            v.y = Mathf.Abs(v.y);
            v.z = Mathf.Abs(v.z);
            return v;
        }

        public static float Fast(this float v)
        {
            if (v > 360f) v %= 360f;

            if (v > 180.0) v = v > 0.0f ? -(360f - v) : 360f - v;

            return v;
        }

        public static Vector2 Fast(this Vector2 v)
        {
            if (v.x > 360f) v.x %= 360f;
            if (v.y > 360f) v.y %= 360f;

            if (v.x > 180.0) v.x = v.x > 0.0f ? -(360f - v.x) : 360f - v.x;
            if (v.y > 180.0) v.y = v.y > 0.0f ? -(360f - v.y) : 360f - v.y;

            return v;
        }

        public static Vector3 Fast(this Vector3 v)
        {
            if (v.x > 360f) v.x %= 360f;
            if (v.y > 360f) v.y %= 360f;
            if (v.z > 360f) v.z %= 360f;

            if (v.x > 180.0) v.x = v.x > 0.0f ? -(360f - v.x) : 360f - v.x;
            if (v.y > 180.0) v.y = v.y > 0.0f ? -(360f - v.y) : 360f - v.y;
            if (v.z > 180.0) v.z = v.z > 0.0f ? -(360f - v.z) : 360f - v.z;

            return v;
        }

        public static bool AnyLessThan(this Vector3 v, float value)
        {
            return v.x < value || v.y < value || v.z < value;
        }

        public static bool AnyGreaterThan(this Vector3 v, float value)
        {
            return v.x > value || v.y > value || v.z > value;
        }

        public static bool AnyXZGreaterThan(this Vector3 v, float value)
        {
            return v.x > value || v.z > value;
        }

        public static bool AllLessThan(this Vector3 v, float value)
        {
            return v.x < value && v.y < value && v.z < value;
        }

        public static bool AllGreaterThan(this Vector3 v, float value)
        {
            return v.x > value && v.y > value && v.z > value;
        }

        public static float Smallest(this Vector3 v)
        {
            if (v.x < v.y && v.x < v.z) return v.x;
            if (v.y < v.x && v.y < v.z) return v.y;
            return v.z;
        }

        public static float Biggest(this Vector3 v)
        {
            if (v.x > v.y && v.x > v.z) return v.x;
            if (v.y > v.x && v.y > v.z) return v.y;
            return v.z;
        }

        public static Vector3 MultiplyXYZ(this Vector3 v, Vector3 value)
        {
            v.x *= value.x;
            v.y *= value.y;
            v.z *= value.z;
            return v;
        }

        public static Vector3 MultiplyXYZ(this Vector3 v, float x, float y, float z)
        {
            v.x *= x;
            v.y *= y;
            v.z *= z;
            return v;
        }
    }
}