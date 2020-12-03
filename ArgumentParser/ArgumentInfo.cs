using System.Reflection;
using ArgumentParser.Attributes;

namespace ArgumentParser
{
    internal class ArgumentInfo
    {
        public ArgumentInfo(PropertyInfo propInfo, ArgumentClassAttribute attribute)
        {
            PropertyInfo = propInfo;
            Attribute = propInfo?.GetCustomAttribute<ArgumentAttribute>();
            m_switchPrefix = attribute?.Switch;
            m_valuePrefix = attribute?.Value;
        }

        public PropertyInfo PropertyInfo { get; }

        public ArgumentAttribute Attribute { get; }

        private readonly string m_switchPrefix;
        private readonly string m_valuePrefix;

        public string ArgumentName => GetArgumentName();

        public string ArgumentAbbreviatedName => WithPrefix(Attribute?.Abbreviation);

        private string GetArgumentName()
        {
            var argumentName = Attribute?.Name;
            if (string.IsNullOrEmpty(argumentName))
            {
                argumentName = PropertyInfo?.Name;
            }

            return WithPrefix(argumentName);
        }

        private string WithPrefix(string name)
        {
            return $"{GetPrefix()}{name?.ToLower()}";
        }

        private string GetPrefix()
        {
            return Attribute.Type == ArgumentType.Value
                ? m_valuePrefix
                : m_switchPrefix;
        }
    }
}
