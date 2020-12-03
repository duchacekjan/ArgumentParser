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
            new object[]{ new[] {"--r" }, true },
            new object[]{ new[] {"-a"}, false },
            new object[]{ new[] {"-a", "--r" }, true }
        };

        public static IEnumerable<object[]> EnumParsedArguments => new[]
        {
            new object[]{ new[] {"-c", "release" }, Config.Release },
            new object[]{ new[] {"-configuration", "release" }, Config.Release },
            new object[]{ new[] {"-a", "-configuration", "Release" }, Config.Release },
            new object[]{ new[] {"-c", "debug" }, Config.Debug },
            new object[]{ new[] {"-configuration", "debug" }, Config.Debug },
            new object[]{ new[] {"-a", "-configuration", "Debug" }, Config.Debug }
        };

        public static IEnumerable<object[]> RequiredParsedArgumentString => new[]
        {
            new object[]{ "-a \"test required parameter\"" , "test required parameter" }
        };

        public static IEnumerable<object[]> SwitchParsedArgumentString => new[]
        {
            new object[]{ "--r" , true },
            new object[]{ string.Empty, false },
            new object[]{ "-a --r", true },
            new object[]{ "-a \"some test\" --r", true }
        };

        public static IEnumerable<object[]> EnumParsedArgumentString => new[]
        {
            new object[]{ "-c release" , Config.Release },
            new object[]{ "-configuration release" , Config.Release },
            new object[]{  "-a -configuration Release \"some test\"", Config.Release },
            new object[]{ "-c debug", Config.Debug },
            new object[]{ "-configuration debug", Config.Debug },
            new object[]{ "-a -configuration Debug \"some test\"", Config.Debug }
        };

        private static IParser<Arguments> GetSut(bool ignoreRequired = false)
        {
            return new Parser<Arguments>
            {
                IgnoreRequired = ignoreRequired
            };
        }
    }
}
