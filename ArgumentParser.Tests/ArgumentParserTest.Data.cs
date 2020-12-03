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

        public static IEnumerable<object[]> RequiredParsedArguments => new[]
        {
            new object[]{ new[] {"-a", "test required parameter" }, "test required parameter" }
        };

        public static IEnumerable<object[]> SwitchParsedArguments => new[]
        {
            new object[]{ new[] {"-a", "test required parameter", "--r" }, true },
            new object[]{ new[] {"-a", "test required parameter" }, false },
            new object[]{ new[] {"-a", "--r" }, true }
        };

        public static IEnumerable<object[]> EnumParsedArguments => new[]
        {
            new object[]{ new[] {"-a", "test required parameter", "-c", "release" }, Config.Release },
            new object[]{ new[] {"-a", "test required parameter", "-configuration", "release" }, Config.Release },
            new object[]{ new[] {"-a", "-configuration", "Release" }, Config.Release },
            new object[]{ new[] {"-a", "test required parameter", "-c", "debug" }, Config.Debug },
            new object[]{ new[] {"-a", "test required parameter", "-configuration", "debug" }, Config.Debug },
            new object[]{ new[] {"-a", "-configuration", "Debug" }, Config.Debug }
        };
    }
}
