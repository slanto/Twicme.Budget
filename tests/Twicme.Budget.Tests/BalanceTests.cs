using FluentAssertions;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class BalanceTests
    {
        [Fact]
        public void GivenRevenues_WhenTotalRevenueIsCalled_ThenTotalRevenueIsCalculated()
        {
            var sut = new Balance(
                new Revenue(Money.CreateZloty(1500), RevenueType.Bonus),
                new Revenue(Money.CreateZloty(120), RevenueType.Rental),
                new Revenue(Money.CreateZloty(500), RevenueType.Plus500),
                new Revenue(Money.CreateZloty(3250.50M), RevenueType.Salary)
            );

            sut.TotalRevenue.Should().BeEquivalentTo(Money.CreateZloty(5370.5M));
        }
    }
}