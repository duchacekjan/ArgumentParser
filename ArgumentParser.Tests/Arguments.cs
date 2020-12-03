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
    }
}
