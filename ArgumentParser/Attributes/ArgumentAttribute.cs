using System;

namespace ArgumentParser.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ArgumentAttribute : Attribute
    {
        public ArgumentAttribute(ArgumentType type, string name = null)
        {
            Type = type;
            Name = name;
        }

        public ArgumentType Type { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public bool IsRequired { get; set; }

        public string Default { get; set; }
    }
}
