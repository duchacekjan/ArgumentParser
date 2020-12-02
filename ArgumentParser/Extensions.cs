using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgumentParser
{
    public static class Extensions
    {
        public static IEnumerable<T> GetAttributes<T>(this Type type, bool inherited = false)
            where T : Attribute
        {
            return type?.GetCustomAttributes(inherited).OfType<T>() ?? new List<T>();
        }
    }
}
