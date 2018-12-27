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
        
        [Fact]
        public void GivenExpenses_WhenTotalExpensesIsCalled_ThenTotalExpensesIsCalculated()
        {
            var sut = new Balance(
                new Expense(Money.CreateZloty(1500)),
                new Expense(Money.CreateZloty(120)),
                new Expense(Money.CreateZloty(500)),
                new Expense(Money.CreateZloty(1250.50M))
            );

            sut.TotalExpense.Should().BeEquivalentTo(Money.CreateZloty(3370.5M));
        }
    }
}