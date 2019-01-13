using FluentAssertions;
using Twicme.Budget.FinancialReport;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class TotalRevenueTests
    {
        private readonly Budget _budget;

        public TotalRevenueTests()
        {
            _budget = new BudgetTestDataBuilder().Build();
        }

        [Fact]
        public void GivenBudget_WhenTotalRevenueIsCalled_ThenTotalAmountIsCalculated()
        {
            var totalRevenue = new TotalRevenue(_budget);
            totalRevenue.Amount.Should().Be(Amount.Create(2250.55M, _budget.BaseCurrency));
        }
        
        [Fact]
        public void GivenBudget_WhenTotalRevenueForSpecificTypeIsCalled_ThenAmountIsCalculated()
        {
            var totalRevenue = new TotalRevenue(_budget);
            
            totalRevenue.For(e => e.Type == RevenueType.Bonus)
                .Amount.Should().Be(Amount.Create(0, _budget.BaseCurrency));
        }
    }
}