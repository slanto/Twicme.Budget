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
        
        [Fact]
        public void
            GivenPlannedAndActualBudgets_WhenTotalExpenseIsCalled_ThenTotalExpenseIsCalculated()
        {
            var plannedBudget = new Budget(Month.Create(2018, MonthName.May), Currency.PLN)
                .WithRevenue(new Money(Amount.Create(100, Currency.PLN), Category.Salary))
                .WithRevenue(new Money(Amount.Create(100, Currency.PLN), Category.Salary))
                .WithExpense(new Money(Amount.Create(-10, Currency.PLN), Category.BasicExpenditure))
                .WithExpense(new Money(Amount.Create(-60, Currency.PLN), Category.CarAndTransport));
            
            var actualBudget = new Budget(Month.Create(2018, MonthName.May), Currency.PLN)
                .WithRevenue(new Money(Amount.Create(100.90m, Currency.PLN), Category.Salary))
                .WithRevenue(new Money(Amount.Create(90, Currency.PLN), Category.Salary))
                .WithExpense(new Money(Amount.Create(-8, Currency.PLN), Category.BasicExpenditure))
                .WithExpense(new Money(Amount.Create(-50, Currency.PLN), Category.CarAndTransport));
            
            var totalExpense = new TotalExpense(plannedBudget, actualBudget);

            totalExpense.Amount.Should().Be(Amount.Create(12, Currency.PLN));
        }
    }
}