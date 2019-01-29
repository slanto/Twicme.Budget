using FluentAssertions;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class DescriptionTests
    {
        [Fact]
        public void GivenCorrectInputData_WhenIsConstructed_ThenDescriptionIsCreated()
        {
            var description = new Description("expense description");

            description.Content.Should().Be("expense description");
        }
        
        [Theory]
        [InlineData("expense description", "description", true)]
        [InlineData("expense description", "descript", true)]
        [InlineData("expense description", "Descript", true)]
        [InlineData("expense description", "other", false)]
        public void GivenDescription_WhenContainsIsCalled_ThenTrueIsReturned(string content, string phrase, bool result)
        {
            var description = new Description(content);

            var contains = description.Contains(phrase);

            contains.Should().Be(result);
        }
    }
}