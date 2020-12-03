using System.Collections.Generic;

namespace ArgumentParser.Tests
{
    public partial class ArgumentParserTest
    {
        public static IEnumerable<object[]> NoRawArguments => new[]
        {
            new object[]{ new string[0]},
            new object[]{ null }
        };
    }
}
