using FluentAssertions;
using System;
using Xunit;

namespace JParser.Tests
{
    public class TestJparser
    {
        [Fact]
        public void Parse_Should_Parse_Json_Data_Correctly()
        {
            // Arrange
            var input = "";
            var expected = "";

            // Act
            var actual = new JParser().Parse(input);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
