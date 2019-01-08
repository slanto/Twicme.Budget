using FluentAssertions;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class TotalBalanceTests
    {
        private readonly Budget _budget;

        public TotalBalanceTests()
        {
            _budget = new BudgetTestDataBuilder().Build();
        }

        [Fact]
        public void GivenBudget_WhenTotalBalanceIsCalled_ThenAmountIsCalculated()
        {
            var totalBalance = new TotalBalance(_budget);
            
            totalBalance.Value.Should().Be(Amount.Create(2149.45M, _budget.BaseCurrency));
        }
    }
}