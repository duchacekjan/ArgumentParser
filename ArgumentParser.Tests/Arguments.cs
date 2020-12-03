using ArgumentParser.Attributes;

namespace ArgumentParser.Tests
{
    public class Arguments
    {
        [Argument(ArgumentType.Value, Name = "a", IsRequired = true)]
        public string Text { get; set; }

        [Argument(ArgumentType.Value, Abbreviation = "t")]
        public string Test { get; set; }

        [Argument(ArgumentType.Switch, Abbreviation = "r")]
        public bool IsRequired { get; set; }

        [Argument(ArgumentType.Value, Abbreviation = "c")]
        public Config Configuration { get; set; }
    }
}
