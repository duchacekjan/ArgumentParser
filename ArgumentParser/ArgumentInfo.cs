using System.Reflection;
using ArgumentParser.Attributes;

namespace ArgumentParser
{
    internal class ArgumentInfo
    {
        public ArgumentInfo(PropertyInfo propInfo)
        {
            PropertyInfo = propInfo;
            Attribute = propInfo?.GetCustomAttribute<ArgumentAttribute>();
        }

        public PropertyInfo PropertyInfo { get; }

        public ArgumentAttribute Attribute { get; }

        public string ArgumentName => GetArgumentName();

        public string ArgumentAbbreviatedName => Attribute?.Abbreviation?.ToLower();

        private string GetArgumentName()
        {
            var argumentName = Attribute?.Name;
            if (string.IsNullOrEmpty(argumentName))
            {
                argumentName = PropertyInfo?.Name;
            }

            return argumentName?.ToLower();
        }
    }
}
