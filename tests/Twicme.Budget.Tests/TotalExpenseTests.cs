using FluentAssertions;
using Twicme.Budget.FinancialReport;
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
            
            totalExpense.Amount.Should().Be(Amount.Create(-101.10M, _budget.BaseCurrency));
        }

        [Fact]
        public void GivenBudget_WhenTotalExpenseForSpecificTypeIsCalled_ThenAmountIsCalculated()
        {
            var totalExpense = new TotalExpense(_budget);

            totalExpense.For(e => e.Category == Category.HomeAndBills)
                .Amount.Should().Be(Amount.Create(-50.55M, _budget.BaseCurrency));
        }
    }
}