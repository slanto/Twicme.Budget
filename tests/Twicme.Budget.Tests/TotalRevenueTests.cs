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

            totalRevenue.For(e => e.Category == Category.HomeAndBills)
                .Amount.Should().Be(Amount.Create(0, _budget.BaseCurrency));
        }

        [Fact]
        public void
            GivenPlannedAndActualBudgets_WhenTotalRevenueIsCalled_ThenTotalRevenueIsCalculated()
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
            
            var totalBalance = new TotalRevenue(plannedBudget, actualBudget);

            totalBalance.Amount.Should().Be(Amount.Create(-9.1m, Currency.PLN));
        }
    }
}