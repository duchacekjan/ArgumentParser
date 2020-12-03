using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ArgumentParser.Attributes;

namespace ArgumentParser
{
    public class Parser<T>: IParser<T>
        where T : class, new()
    {
        private readonly ArgumentClassAttribute m_attribute;
        private readonly List<string> m_requiredError = new List<string>();
        private readonly List<string> m_rawArguments = new List<string>();

        public Parser()
        {
            m_attribute = typeof(T).GetAttributes<ArgumentClassAttribute>()?.FirstOrDefault()
                ?? new ArgumentClassAttribute();
        }

        public T Parse(params string[] args)
        {
            m_rawArguments.Clear();
            m_rawArguments.AddRange(args?? new string[0]);
            m_requiredError.Clear();

            var result = default(T);
            if (args?.Length > 0)
            {
                result = AssignArguments(args.ToList());
            }

            if (m_requiredError.Any())
            {
                throw new Exception($"Missing required arguments: {string.Join(", ", m_requiredError)}");
            }

            return result;
        }

        public IReadOnlyCollection<string> RawArguments => m_rawArguments.AsReadOnly();

        private T AssignArguments(List<string> args)
        {
            var result = new T();
            foreach (var propertyWithAttribute in GetProperties())
            {
                var value = GetArgumentValue(propertyWithAttribute, args);
            }

            return result;
        }

        private object GetArgumentValue(ArgumentInfo info, List<string> args)
        {
            return null;
        }

        private IEnumerable<ArgumentInfo> GetProperties()
        {
            return typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(s => new ArgumentInfo(s, m_attribute))
                .Where(w => w.Attribute != null);
        }
    }
}
