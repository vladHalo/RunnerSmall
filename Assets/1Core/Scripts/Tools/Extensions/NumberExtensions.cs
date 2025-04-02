using System;
using UnityEngine;
using Random = UnityEngine.Random;

#pragma warning disable 0649
namespace Core.Scripts.Tools.Extensions
{
    public static class NumberExtensions
    {
        public static bool ToBool(this int value) => value > 0;

        public static int ToInt(this bool value) => value ? 1 : 0;

        public static int FloorToInt(this float t, int length)
        {
            return Mathf.FloorToInt(t);
        }

        public static int WithRandomSign(this int value, float negativeProbability = 0.5f)
        {
            return Random.value < negativeProbability ? -value : value;
        }

        public static int Repeat(this int t, int length)
        {
            return Mathf.Clamp(t - Mathf.FloorToInt((float)t / length) * length, 0, length);
        }
        
        public static int Repeat(this int value, int length, int step)
        {
            // length = 4
            // step = 3
            // 0 => 0
            // 1 => 0
            // 2 => 0
            // 3 => 1
            // 4 => 1
            // 5 => 1
            // 6 => 2
            // 7 => 2
            // 8 => 2
            // 9 => 3
            // 10 => 3
            // 11 => 3
            // 12 => 0
            // 13 => 0
            // ...
            return ((float)value / step).FloorToInt().Repeat(length);
        }

        public static int RoundToNearest(this int num, int nearest)
        {
            return ((float)num).RoundToNearestInt(nearest);
        }

        public static int RoundToNearestInt(this float num, int nearest)
        {
            return Mathf.RoundToInt(num / nearest) * nearest;
        }

        public static float RoundToNearest(this float num, float nearest)
        {
            return Mathf.Round(num / nearest) * nearest;
        }

        public static double RoundToNearest(this double num, float nearest)
        {
            return Math.Round(num / nearest) * nearest;
        }

        public static float Sign(this float num)
        {
            return num >= 0 ? 1 : -1;
        }

        public static int OppositeZero(this int num)
        {
            return num == 0 ? 1 : 0;
        }
        
        public static float OppositeZero(this float num)
        {
            return num == 0 ? 1 : 0;
        }
        
        public static float Remap(this float num, float iMin, float iMax, Vector2 output)
        {
            return num.Remap(iMin, iMax, output.x, output.y);
        }

        public static float Remap(this float num, Vector2 input, float oMin, float oMax)
        {
            return num.Remap(input.x, input.y, oMin, oMax);
        }

        public static float Remap(this float num, Vector2 input, Vector2 output)
        {
            return num.Remap(input.x, input.y, output.x, output.y);
        }

        public static float Abs(this float num)
        {
            return Mathf.Abs(num);
        }

        public static float DistanceTo(this float a, float b) => Mathf.Sqrt(Mathf.Pow(a - b, 2));

        public static int Clamp(this int v, int min, int max) => Mathf.Clamp(v, min, max);
        public static int Clamp(this int v, Vector2Int minMax) => Mathf.Clamp(v, minMax.x, minMax.y);
        public static float Clamp(this float v, Vector2 minMax) => Mathf.Clamp(v, minMax.x, minMax.y);

        public static bool IsBetweenRange(this float thisValue, float value1, float value2)
        {
            return thisValue >= Mathf.Min(value1, value2) && thisValue <= Mathf.Max(value1, value2);
        }

        public static bool IsBetweenRange(this int thisValue, int value1, int value2)
        {
            return thisValue >= Mathf.Min(value1, value2) && thisValue <= Mathf.Max(value1, value2);
        }

        public static int RandomDelta(this int thisValue, int delta) =>
            Random.Range(thisValue - delta, thisValue + delta + 1);

        public static int RandomFrom(this int thisValue, int from) => Random.Range(from, thisValue + 1);
        public static int RandomTo(this int thisValue, int to) => Random.Range(thisValue, to + 1);
        public static int RandomFromZero(this int thisValue) => Random.Range(0, thisValue + 1);
        public static int RandomFromNegative(this int thisValue) => Random.Range(-thisValue, thisValue + 1);
        public static float RandomFromZero(this float thisValue) => Random.Range(0, thisValue);
        public static float RandomFromNegative(this float thisValue) => Random.Range(-thisValue, thisValue);

        public static float MinClamp(this float thisValue, float value) => Mathf.Max(thisValue, value);
        public static float MaxClamp(this float thisValue, float value) => Mathf.Min(thisValue, value);
        public static int MinClamp(this int thisValue, int value) => Mathf.Max(thisValue, value);
        public static int MaxClamp(this int thisValue, int value) => Mathf.Min(thisValue, value);
        public static double MinClamp(this double thisValue, double value) => Math.Max(thisValue, value);
        public static double MaxClamp(this double thisValue, double value) => Math.Min(thisValue, value);
    }
}