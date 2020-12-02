using ArgumentParser.Attributes;

namespace ArgumentParser.Demo
{
    public class Arguments
    {
        public enum Config
        {
            Debug,
            Release
        }

        [Argument(ArgumentType.Value)]
        public string Output { get; set; }

        [Argument(ArgumentType.Switch)]
        public bool IsRequired { get; set; }

        [Argument(ArgumentType.Value, Abbreviation = "c")]
        public Config Configuration { get; set; }
    }
}
