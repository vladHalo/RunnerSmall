using System.Text.RegularExpressions;

#pragma warning disable 0649
namespace Core.Scripts.Tools.Extensions
{
    public static class StringExtensions
    {
        public static string SplitCamelCase(this string input)
        {
            return Regex.Replace(input, "([A-Z])", " $1", RegexOptions.Compiled).Trim();
        }
    }
}