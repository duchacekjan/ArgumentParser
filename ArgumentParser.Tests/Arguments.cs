using ArgumentParser.Attributes;

namespace ArgumentParser.Tests
{
    [ArgumentClass]
    public class Arguments
    {
        [Argument(ArgumentType.Value, Name = "a")]
        public string Text { get; set; }

        [Argument(ArgumentType.Value, Abbreviation = "t")]
        public string Test { get; set; }

        [Argument(ArgumentType.Switch, Abbreviation = "r")]
        public bool IsRequired { get; set; }
    }
}
