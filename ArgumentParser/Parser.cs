using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ArgumentParser.Attributes;

namespace ArgumentParser
{
    public class Parser<T> : IParser<T>
        where T : class, new()
    {
        private readonly ArgumentClassAttribute m_attribute;
        private readonly List<string> m_requiredError = new List<string>();
        private readonly List<string> m_rawArguments = new List<string>();
        private readonly List<Argument> m_arguments = new List<Argument>();

        public Parser()
        {
            m_attribute = typeof(T).GetAttributes<ArgumentClassAttribute>()?.FirstOrDefault()
                ?? new ArgumentClassAttribute();
        }

        public T Parse(params string[] args)
        {
            m_rawArguments.Clear();
            m_rawArguments.AddRange(args ?? new string[0]);
            m_requiredError.Clear();

            var result = default(T);
            if (m_rawArguments.Count > 0)
            {
                result = AssignArguments();
            }

            if (m_requiredError.Any())
            {
                throw new Exception($"Missing required arguments: {string.Join(", ", m_requiredError)}");
            }

            return result;
        }

        public IReadOnlyCollection<string> RawArguments => m_rawArguments.AsReadOnly();

        public IReadOnlyCollection<Argument> Arguments => m_arguments.AsReadOnly();

        private T AssignArguments()
        {
            var result = new T();

            m_arguments.Clear();
            m_arguments.AddRange(GetProperties().Select(s => GetArgument(s, result)).Where(w => w != null));

            return result;
        }

        private Argument GetArgument(ArgumentInfo info, T data)
        {
            Argument result = null;

            return result;
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
