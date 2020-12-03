using FluentAssertions;
using Xunit;

namespace ArgumentParser.Tests
{
    public partial class ArgumentParserTest
    {
        private static readonly IParser<Arguments> m_sut = new Parser<Arguments>();
        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("test", "t")]
        public void Should_HaveCorrect_NumberOfRawArguments(params string[] args)
        {
            m_sut.Parse(args);
            m_sut.RawArguments.Should().HaveCount(args.Length);
        }

        [Theory]
        [MemberData(nameof(NoRawArguments))]
        public void Should_HaveNoRawArguments(string[] args)
        {
            m_sut.Parse(args);
            m_sut.RawArguments.Should().HaveCount(0);
        }

        [Theory]
        [InlineData(1, "-a")]
        [InlineData(2, "-a", "-test", "-t")]
        [InlineData(3, "-a", "-test", "--r")]
        public void Should_HaveCorrect_NumberOfArguments(int expected, params string[] args)
        {
            m_sut.Parse(args);
            m_sut.Arguments.Should().HaveCount(expected);
        }

        [Theory]
        [MemberData(nameof(RequiredParsedArguments))]
        public void Should_HaveCorrectRequired_ParsedArguments(string[] args, string expected)
        {
            var actual = m_sut.Parse(args);
            actual.Text.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(SwitchParsedArguments))]
        public void Should_HaveCorrectSwitch_ParsedArguments(string[] args, bool expected)
        {
            var actual = m_sut.Parse(args);
            actual.IsRequired.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(EnumParsedArguments))]
        public void Should_HaveCorrectEnums_ParsedArguments(string[] args, Config expected)
        {
            var actual = m_sut.Parse(args);
            actual.Configuration.Should().Be(expected);
        }
    }
}
