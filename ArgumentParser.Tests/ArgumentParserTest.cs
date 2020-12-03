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
        public void Should_HaveCorrect_NumberOfRawArguments(params string[] args)
        {
            var sut = GetSut(true);
            sut.Parse(args);
            sut.RawArguments.Should().HaveCount(args.Length);
        }

        [Theory]
        [MemberData(nameof(NoRawArguments))]
        public void Should_HaveNoRawArguments(string[] args)
        {
            var sut = GetSut();
            sut.Parse(args);
            sut.RawArguments.Should().HaveCount(0);
        }

        [Theory]
        [InlineData(1, "-a")]
        [InlineData(1, "-test", "-t")]
        [InlineData(2, "-test", "--r")]
        public void Should_HaveCorrect_NumberOfArguments(int expected, params string[] args)
        {
            var sut = GetSut(true);
            sut.Parse(args);
            sut.Arguments.Should().HaveCount(expected);
        }

        [Theory]
        [MemberData(nameof(RequiredParsedArguments))]
        public void Should_HaveCorrectRequired_ParsedArguments(string[] args, string expected)
        {
            var sut = GetSut();
            var actual = sut.Parse(args);
            actual.Text.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(SwitchParsedArguments))]
        public void Should_HaveCorrectSwitch_ParsedArguments(string[] args, bool expected)
        {
            var sut = GetSut();
            var actual = sut.Parse(args);
            actual.IsRequired.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(EnumParsedArguments))]
        public void Should_HaveCorrectEnums_ParsedArguments(string[] args, Config expected)
        {
            var sut = GetSut();
            var actual = sut.Parse(args);
            actual.Configuration.Should().Be(expected);
        }

        [Theory]
        [InlineData("c -a --k", new[] { "c", "-a", "--k" })]
        [InlineData("-c \"some test\" --a k", new[] {"--a", "-c", "some test", "k" })]
        public void Should_Split_StringArguments(string test, string[] expected)
        {
            var actual = test.ToArgs();
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
