using System;
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
            _budget = new BudgetTestDataBuilder().Build();
        }

        [Fact]
        public void GivenBudget_WhenTotalBalanceIsCalled_ThenAmountIsCalculated()
        {
            var totalBalance = new TotalBalance(_budget);
            
            totalBalance.Amount.Should().Be(Amount.Create(2149.45M, _budget.BaseCurrency));
        }

        [Fact]
        public void GivenPlannedAndActualBudgets_WhenTotalBalanceIsCalled_ThenTotalBalanceOfRevenueAndExpensesAreCalculated()
        {
            var plannedBudget = new Budget(Month.Create(2018, MonthName.May), Currency.PLN, DateTime.Today)
                .WithRevenue(new Money(Amount.Create(100, Currency.PLN), Category.Salary))
                .WithRevenue(new Money(Amount.Create(100, Currency.PLN), Category.Salary))
                .WithExpense(new Money(Amount.Create(-10, Currency.PLN), Category.BasicExpenditure))
                .WithExpense(new Money(Amount.Create(-60, Currency.PLN), Category.CarAndTransport));
            
            var actualBudget = new Budget(Month.Create(2018, MonthName.May), Currency.PLN, DateTime.Today)
                .WithRevenue(new Money(Amount.Create(100.90m, Currency.PLN), Category.Salary))
                .WithRevenue(new Money(Amount.Create(90, Currency.PLN), Category.Salary))
                .WithExpense(new Money(Amount.Create(-8, Currency.PLN), Category.BasicExpenditure))
                .WithExpense(new Money(Amount.Create(-50, Currency.PLN), Category.CarAndTransport));
            
            var totalBalance = new TotalBalance(plannedBudget, actualBudget);

            totalBalance.Amount.Should().Be(Amount.Create(-2.90m, Currency.PLN));
        }
    }
}