using FluentAssertions;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class RevenueTypeTests
    {
        [Fact]
        public void GivenTwoRevenueTypes_WhenTheyAreCompared_ThenTheyAreEqual()
        {
            var type1 = RevenueType.Create("revenue-type");
            var type2 = RevenueType.Create("revenue-type");
            
            type1.Should().BeEquivalentTo(type2);
        }
    }
}