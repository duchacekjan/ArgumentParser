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

        public string AttributeName => GetAttributeName();

        private string GetAttributeName()
        {
            var argumentName = Attribute?.Name;
            if (string.IsNullOrEmpty(argumentName))
            {
                argumentName = PropertyInfo?.Name;
            }

            return argumentName;
        }
    }
}
