using FluentAssertions;
using Xunit;

namespace ArgumentParser.Tests
{
    public class ArgumentParserTest
    {
        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("test", "t")]
        public void Should_HaveArguments(params string[] args)
        {
            var sut = new Parser<Arguments>();
            sut.Parse(args);
            sut.RawArgs.Should().HaveCount(args.Length);
        }
    }
}
