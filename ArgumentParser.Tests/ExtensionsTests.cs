using FluentAssertions;
using Xunit;

namespace ArgumentParser.Tests
{
    public class ExtensionsTests
    {
        [Theory]
        [InlineData("c -a --k", new[] { "c", "-a", "--k" })]
        [InlineData("-c \"some test\" --a k -self-contained --self-contained", new[] { "--a", "-c", "some test", "k", "-self-contained", "--self-contained" })]
        public void Should_Split_StringArguments(string test, string[] expected)
        {
            var actual = test.ToArgs();
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
