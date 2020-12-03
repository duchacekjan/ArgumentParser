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
        [InlineData(0, "")]
        [InlineData(1, "a")]
        [InlineData(1, "test", "t")]
        [InlineData(2, "test", "r")]
        public void Should_HaveCorrect_NumberOfArguments(int count, params string[] args)
        {
            m_sut.Parse(args);
            m_sut.Arguments.Should().HaveCount(count);
        }
    }
}
