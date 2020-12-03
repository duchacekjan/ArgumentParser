using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            var index = GetArgumentIndex(info);
            if (index >= 0)
            {
                result = new Argument
                {
                    Name = info.ArgumentName,
                    Abbreviation = info.ArgumentAbbreviatedName,
                    RawValue = GetRawArgumentValue(index, info.Attribute.Type)
                };


                if (info.Attribute.IsRequired && string.IsNullOrEmpty(result.RawValue))
                {
                    m_requiredError.Add(info.ArgumentName);
                }

                if (info.Attribute.Type != ArgumentType.Switch || !string.IsNullOrEmpty(result.RawValue))
                {
                    var value = GetArgumentValue(result.RawValue, info.PropertyInfo.PropertyType);
                    info.PropertyInfo.SetValue(data, value);
                }
            }
            else if (info.Attribute.IsRequired)
            {
                m_requiredError.Add(info.ArgumentName);
            }

            return result;
        }

        private object GetArgumentValue(string rawValue, Type propertyType)
        {
            object result = rawValue;
            if (propertyType != typeof(string) && propertyType.IsValueType)
            {
                var typeConverter = TypeDescriptor.GetConverter(propertyType);
                result = typeConverter.ConvertFromString(rawValue);
            }

            return result;
        }

        private string GetRawArgumentValue(int index, ArgumentType argumentType)
        {
            string result = null;
            if (argumentType == ArgumentType.Switch)
            {
                result = "true";
            }
            else
            {
                if (index + 1 < m_rawArguments.Count)
                {
                    result = m_rawArguments[index + 1];
                }
            }

            return result;
        }

        private int GetArgumentIndex(ArgumentInfo info)
        {
            var args = m_rawArguments
                .Select(s => s?.ToLower())
                .ToList();
            var result = args.IndexOf(info.ArgumentName);
            if (result < 0)
            {
                result = args.IndexOf(info.ArgumentAbbreviatedName);
            }

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
