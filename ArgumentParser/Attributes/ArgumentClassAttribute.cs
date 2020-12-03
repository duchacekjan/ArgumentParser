using System;

namespace ArgumentParser.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ArgumentClassAttribute : Attribute
    {
        public ArgumentClassAttribute(string @switch = "--", string value = "-")
        {
            Switch = @switch;
            Value = value;
        }

        public string Switch { get; set; }

        public string Value { get; set; }
    }
}