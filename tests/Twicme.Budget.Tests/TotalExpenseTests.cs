using FluentAssertions;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class TotalExpenseTests
    {
        private readonly Budget _budget;

        public TotalExpenseTests()
        {
            _budget = new BudgetTestDataBuilder().Build();
        }
        
        [Fact]
        public void GivenBudget_WhenTotalExpenseIsCalled_ThenAmountIsCalculated()
        {   
            var totalExpense = new TotalExpense(_budget);
            
            totalExpense.Value.Should().Be(Amount.Create(-101.10M, _budget.BaseCurrency));
        }
    }
}