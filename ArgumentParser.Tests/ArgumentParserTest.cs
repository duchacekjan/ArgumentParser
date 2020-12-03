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
        public void Should_HaveCorrect_NumberOfArguments(params string[] args)
        {
            var sut = new Parser<Arguments>();
            sut.Parse(args);
            sut.RawArguments.Should().HaveCount(args.Length);
        }

        [Theory]
        [MemberData(nameof(NoRawArguments))]
        public void Should_HaveNoArguments(string[] args)
        {
            var sut = new Parser<Arguments>();
            sut.Parse(args);
            sut.RawArguments.Should().HaveCount(0);
        }

        [Theory]
        [InlineData(0, "")]
        [InlineData(1, "a")]
        [InlineData(1, "test", "t")]
        [InlineData(2, "test", "r")]
        public void Should_HaveArgumentPairs_CorrectCount(int count, params string[] args)
        {
            var sut = new Parser<Arguments>();
            sut.Parse(args);
            sut.RawArguments.Should().HaveCount(count);
        }
    }
}
