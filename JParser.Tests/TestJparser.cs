using FluentAssertions;
using Xunit;

namespace JParser.Tests
{
    public class TestJparser
    {
        [Fact]
        public async void Parse_Should_Parse_Json_Data_Correctly()
        {
            // Arrange
            var input = @"{
                          ""FirstName"": ""Arthur"",
                          ""LastName"": ""Bertrand"",
                          ""Address"": {
                              ""StreetName"": ""Gedempte Zalmhaven"",
                              ""Number"": ""4K"",
                              ""City"": {
                                 ""Name"": ""Rotterdam"",
                                 ""Country"": ""The Netherlands""
                              },
                              ""ZipCode"": ""3011 BT""
                          },
                          ""Age"": 35,
                          ""Hobbies"": [
                              ""Fishing"",
                              ""Rowing""
                          ]
                         }";
            var expected = @"{
  ""FirstName"": ""Arthur"",
  ""LastName"": ""Bertrand"",
  ""Age"": 35,
  ""Address"": {
    ""StreetName"": ""Gedempte Zalmhaven"",
    ""Number"": ""4K"",
    ""ZipCode"": ""3011 BT"",
    ""City"": {
      ""Name"": ""Rotterdam"",
      ""Country"": ""The Netherlands""
    }
  },
  ""Hobbies"": [
    ""Fishing"",
    ""Rowing""
  ]
}";

            // Act
            var actual = await new JParser().ParseAsync(input);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}