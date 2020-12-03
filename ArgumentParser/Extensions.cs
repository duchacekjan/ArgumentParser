using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ArgumentParser
{
    public static class Extensions
    {
        private static Regex m_toArgsRE = new Regex(@"((""((?<token>.*?)(?<!\\)"")|(?<token>[\w]+))(\s)*)", RegexOptions.Compiled);
        public static IEnumerable<T> GetAttributes<T>(this Type type, bool inherited = false)
            where T : Attribute
        {
            return type?.GetCustomAttributes(inherited).OfType<T>() ?? new List<T>();
        }

        public static string[] ClearArgs(this string[] args)
        {
            return args?.Select(ClearArg).ToArray();
        }

        public static string[] ToArgs(this string text)
        {
            string[] result = null;
            if (!string.IsNullOrEmpty(text))
            {
                result = m_toArgsRE.Matches(text)
                    .Cast<Match>()
                    .Where(w => w.Groups["token"].Success)
                    .Select(s => s.Groups["token"].Value)
                    .ToArray();
            }

            return result;
        }

        private static string ClearArg(string arg)
        {
            var result = arg;
            if (arg?.StartsWith("--") == true)
            {
                result = arg.Remove(0, 2);
            }
            else if (arg?.StartsWith("-") == true)
            {
                result = arg.Remove(0, 1);
            }

            return result;
        }
    }
}
