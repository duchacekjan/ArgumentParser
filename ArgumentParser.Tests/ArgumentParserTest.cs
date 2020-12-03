using FluentAssertions;
using Xunit;

namespace ArgumentParser.Tests
{
    public partial class ArgumentParserTest
    {
        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("test", "t")]
        public void ShouldArgs_HaveCorrect_NumberOfRawArguments(params string[] args)
        {
            var sut = GetSut(true);
            sut.Parse(args);
            sut.RawArguments.Should().HaveCount(args.Length);
        }

        [Theory]
        [MemberData(nameof(NoRawArguments))]
        public void ShouldArgs_HaveNoRawArguments(string[] args)
        {
            var sut = GetSut();
            sut.Parse(args);
            sut.RawArguments.Should().HaveCount(0);
        }

        [Theory]
        [InlineData(1, "-a")]
        [InlineData(1, "-test", "-t")]
        [InlineData(2, "-test", "--r")]
        public void ShouldArgs_HaveCorrect_NumberOfArguments(int expected, params string[] args)
        {
            var sut = GetSut(true);
            sut.Parse(args);
            sut.Arguments.Should().HaveCount(expected);
        }

        [Theory]
        [MemberData(nameof(RequiredParsedArguments))]
        public void ShouldArgs_HaveCorrectRequired_ParsedArguments(string[] args, string expected)
        {
            var sut = GetSut();
            var actual = sut.Parse(args);
            actual.Text.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(SwitchParsedArguments))]
        public void ShouldArgs_HaveCorrectSwitch_ParsedArguments(string[] args, bool expected)
        {
            var sut = GetSut(true);
            var actual = sut.Parse(args);
            actual.IsRequired.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(EnumParsedArguments))]
        public void ShouldArgs_HaveCorrectEnums_ParsedArguments(string[] args, Config expected)
        {
            var sut = GetSut(true);
            var actual = sut.Parse(args);
            actual.Configuration.Should().Be(expected);
        }

        [Theory]
        [InlineData(null, 0)]
        [InlineData("", 0)]
        [InlineData("test t", 2)]
        [InlineData("test t \"some test\"", 3)]
        public void ShouldString_HaveCorrect_NumberOfRawArguments(string args, int expected)
        {
            var sut = GetSut(true);
            sut.Parse(args);
            sut.RawArguments.Should().HaveCount(expected);
        }

        [Theory]
        [InlineData("-a", 1)]
        [InlineData("-test -t", 1)]
        [InlineData("-test --r", 2)]
        [InlineData("-test --r --self-contained", 3)]
        public void ShouldString_HaveCorrect_NumberOfArguments(string args, int expected)
        {
            var sut = GetSut(true);
            sut.Parse(args);
            sut.Arguments.Should().HaveCount(expected);
        }

        [Theory]
        [MemberData(nameof(RequiredParsedArgumentString))]
        public void ShouldString_HaveCorrectRequired_ParsedArguments(string args, string expected)
        {
            var sut = GetSut();
            var actual = sut.Parse(args);
            actual.Text.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(SwitchParsedArgumentString))]
        public void ShouldString_HaveCorrectSwitch_ParsedArguments(string args, bool expected)
        {
            var sut = GetSut(true);
            var actual = sut.Parse(args);
            actual.IsRequired.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(EnumParsedArgumentString))]
        public void ShouldString_HaveCorrectEnums_ParsedArguments(string args, Config expected)
        {
            var sut = GetSut(true);
            var actual = sut.Parse(args);
            actual.Configuration.Should().Be(expected);
        }
    }
}
