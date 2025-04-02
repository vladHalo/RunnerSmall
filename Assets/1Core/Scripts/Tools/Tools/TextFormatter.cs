using System;
using System.Globalization;
using System.Text;
using UnityEngine;

namespace Core.Scripts.Tools.Tools
{
    public static class TextFormatter
    {
        public static string ToHumanTimeFormat(this float value)
        {
            var timeSpan = TimeSpan.FromSeconds(value);

            var format = @"ss";

            if (timeSpan.Minutes > 0)
            {
                format = @"mm\:ss";
            }

            if (timeSpan.Hours > 0)
            {
                format = @"hh\:mm\:ss";
            }

            if (timeSpan.Days > 0)
            {
                format = @"dd hh\:mm\:ss";
            }

            return TimeSpan.FromSeconds(value).ToString(format);
        }

        public static string ToHumanTimeFormat(this double value)
        {
            var timeSpan = TimeSpan.FromSeconds(value);

            var format = @"ss";

            if (timeSpan.Minutes > 0)
            {
                format = @"mm\:ss";
            }

            if (timeSpan.Hours > 0)
            {
                format = @"hh\:mm\:ss";
            }

            if (timeSpan.Days > 0)
            {
                format = @"dd hh\:mm\:ss";
            }

            return TimeSpan.FromSeconds(value).ToString(format);
        }

        public static string ToIdleFormat(this int value, int digits, int skipThousand, CultureInfo cultureInfo)
        {
            return FormatCoinsForUi(value, digits, skipThousand, cultureInfo);
        }

        public static string ToIdleFormat(this int value, int digits = 2, int skipThousand = 1)
        {
            return FormatCoinsForUi(value, digits, skipThousand, CultureInfo.CurrentCulture);
        }

        public static string ToIdleFormat(this double value, int digits, int skipThousand, CultureInfo cultureInfo)
        {
            return FormatCoinsForUi(value, digits, skipThousand, cultureInfo);
        }

        public static string ToIdleFormat(this double value, int digits = 2, int skipThousand = 1)
        {
            return FormatCoinsForUi(value, digits, skipThousand, CultureInfo.CurrentCulture);
        }

        public static string ToIdleFormat(this float value, int digits, int skipThousand, CultureInfo cultureInfo)
        {
            return FormatCoinsForUi(value, digits, skipThousand, cultureInfo);
        }

        public static string ToIdleFormat(this float value, int digits = 2, int skipThousand = 1)
        {
            return FormatCoinsForUi(value, digits, skipThousand, CultureInfo.CurrentCulture);
        }

        public static string FormatCoinsForUi(double amount, int digits, int skipThousand, CultureInfo cultureInfo)
        {
            if (amount <= 0) return "0";
            // if (amount < 1) return amount.DotFormat(digits, CultureInfo.InvariantCulture);

            var index = (int) (Math.Log10(amount) / 3f);

            if (index < skipThousand)
            {
                digits = 0;
            }

            amount /= Math.Pow(10, index * 3);

            index = Math.Min(index, prefix.Length - 1);

            return string.Concat(amount.DotFormat(digits, cultureInfo), prefix[index]);
        }

        public static string FormatCoinsForUi(float amount, int digits, int skipThousand, CultureInfo cultureInfo)
        {
            if (amount <= 0) return "0";

            var index = (int) (Mathf.Log10(amount) / 3);

            if (index < skipThousand)
            {
                digits = 0;
            }

            amount /= Mathf.Pow(10, index * 3);

            index = Math.Min(Math.Max(0, index), prefix.Length - 1);

            return string.Concat(amount.DotFormat(digits, cultureInfo), prefix[index]);
        }

        public static string FormatCoinsSpeed(float speed) { return string.Concat(speed.ToIdleFormat(), "/ SEC"); }

        public static string AddSpaceBetweenCapitalLetters(string text)
        {
            var str = new StringBuilder();

            for (var i = 0; i < text.Length; i++)
            {
                if (i > 0 && char.IsUpper(text[i]))
                {
                    str.Append(" ");
                }

                str.Append(text[i]);
            }

            return str.ToString();
        }

        #region Periphery Values

        private static string[] prefix =
        {
            "", "K", "M", "G", "T",
            "Aa", "Ab", "Ac", "Ad", "Ae", "Af", "Ag", "Ah", "Ai", "Aj", "Ak", "Al", "Am", "An", "Ao", "Ap", "Aq", "Ar",
            "As", "At", "Au", "Av", "Aw", "Ax", "Ay", "Az",
            "Ba", "Bb", "Bc", "Bd", "Be", "Bf", "Bg", "Bh", "Bi", "Bj", "Bk", "Bl", "Bm", "Bn", "Bo", "Bp", "Bq", "Br",
            "Bs", "Bt", "Bu", "Bv", "Bw", "Bx", "By", "Bz",
            "Ca", "Cb", "Cc", "Cd", "Ce", "Cf", "Cg", "Ch", "Ci", "Cj", "Ck", "Cl", "Cm", "Cn", "Co", "Cp", "Cq", "Cr",
            "Cs", "Ct", "Cu", "Cv", "Cw", "Cx", "Cy", "Cz",
            "Da", "Db", "Dc", "Dd", "De", "Df", "Dg", "Dh", "Di", "Dj", "Dk", "Dl", "Dm", "Dn", "Do", "Dp", "Dq", "Dr",
            "Ds", "Dt", "Du", "Dv", "Dw", "Dx", "Dy", "Dz"
        };

        #endregion

        #region Dot Formatter

        public static string DotFormat(this int value, int digits, CultureInfo cultureInfo)
        {
            return value.ToString(cultureInfo).DotFormat(digits);
        }

        public static string DotFormat(this int value, int digits = 2)
        {
            return DotFormat(value, digits, CultureInfo.CurrentCulture);
        }

        public static string DotFormat(this double value, int digits, CultureInfo cultureInfo)
        {
            return value.ToString(cultureInfo).DotFormat(digits);
        }

        public static string DotFormat(this double value, int digits = 2)
        {
            return DotFormat(value, digits, CultureInfo.CurrentCulture);
        }

        public static string DotFormat(this float value, int digits, CultureInfo cultureInfo)
        {
            return value.ToString(cultureInfo).DotFormat(digits);
        }

        public static string DotFormat(this float value, int digits = 2)
        {
            return DotFormat(value, digits, CultureInfo.CurrentCulture);
        }

        public static string DotFormat(this string value, int digits = 2)
        {
            var separator = value.Contains(",") ? "," : ".";
            return value.DotFormat(separator, digits);
        }

        public static string DotFormat(this string value, string separator = ",", int digits = 2)
        {
            var separatorIndex = value.IndexOf(separator, StringComparison.Ordinal);

            return separatorIndex == -1
                ? value
                : value.Substring(0, Math.Min(value.Length, separatorIndex + digits + 1)).TrimEnd('0')
                    .TrimEnd(separator[0]);
        }

        #endregion
    }
}