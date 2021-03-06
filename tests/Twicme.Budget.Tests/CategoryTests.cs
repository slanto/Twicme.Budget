using FluentAssertions;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class CategoryTests
    {
        [Fact]
        public void GivenTwoCategories_WhenTheyAreCompared_ThenTheyAreEqual()
        {
            var category1 = Category.Create("category");
            var category2 = Category.Create("category");
            
            category1.Should().BeEquivalentTo(category2);
        }

        [Fact]
        public void GivenCorrectInputData_WhenFromIsCalled_ThenCategoryIsReturned()
        {
            var result = Category.From("education");

            result.Should().Be(Category.Education);
        }
    }
}