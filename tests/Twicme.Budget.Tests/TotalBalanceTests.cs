using FluentAssertions;
using Twicme.Budget.FinancialReport;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class TotalBalanceTests
    {
        private readonly Budget _budget;

        public TotalBalanceTests()
        {
            _budget = BudgetTestDataBuilder.Build();
        }

        [Fact]
        public void GivenBudget_WhenTotalBalanceIsCalled_ThenAmountIsCalculated()
        {
            var totalBalance = new TotalBalance(_budget);
            
            totalBalance.Amount.Should().Be(Amount.Create(2149.45M, _budget.BaseCurrency));
        }
    }
}