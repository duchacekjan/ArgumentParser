using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ArgumentParser
{
    public static class Extensions
    {
        private static Regex m_toArgsRE = new Regex(@"(?<="")\w[\w\s]*(?="")|[\w.-]+|""[\w\s]*""", RegexOptions.Compiled);

        public static IEnumerable<T> GetAttributes<T>(this Type type, bool inherited = false)
            where T : Attribute
        {
            return type?.GetCustomAttributes(inherited).OfType<T>() ?? new List<T>();
        }

        public static string[] ToArgs(this string text)
        {
            string[] result = new string[0];
            if (!string.IsNullOrEmpty(text))
            {
                result = m_toArgsRE.Matches(text)
                    .Cast<Match>()
                    .Where(w => w.Success)
                    .Select(ClearMatch)
                    .ToArray();
            }

            return result;
        }

        private static string ClearMatch(Match match)
        {
            var result = match.Value;
            if (result.StartsWith("\""))
            {
                result = result.Remove(0, 1);
            }

            if (result.EndsWith("\""))
            {
                result = result.Remove(result.Length - 1, 1);
            }

            return result;
        }
    }
}
