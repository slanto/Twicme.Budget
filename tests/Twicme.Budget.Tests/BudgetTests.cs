using System;
using System.Collections.Immutable;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using Twicme.Budget.FinancialReport;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class BudgetTests
    {
        private static Budget Budget =>
            new BudgetTestDataBuilder()
                .WithCreated(Created)
                .Build();
        
        private static DateTimeOffset Created => 
            new DateTimeOffset(2018, 2, 10, 6, 12, 0, TimeSpan.Zero);
        
        [Fact]
        public void GivenCorrectInputData_WhenConstructorIsCalled_ThenBudgetIsInitialized()
        {
            var sut = new Budget(MonthName.July, 2012, Currency.PLN, Created, ImmutableList<IMoney>.Empty);

            sut.Should().NotBeNull();
            sut.Created.Should().Be(Created);
            sut.MonthName.Should().Be(MonthName.July);
            sut.Year.Should().Be(2012);
            sut.Moneys.Should().BeEmpty();
        }

        [Fact]
        public void GivenBudget_WhenMoneyIsAdded_ThenBalanceContainsAdditionalAmount()
        {
            var revenue = new Revenue(Amount.Create(200, Currency.PLN), RevenueType.Bonus, Created);
            
            var budget = Budget.Add(revenue);
            budget.Moneys.Should().Contain(revenue);
        }
        
        [Fact]
        public void GivenBudgetInPLN_WhenAmountInUSDIsAdded_ThenExceptionIsThrown()
        {
            Func<Budget> func = () => Budget.Add(
                new Revenue(Amount.Create(200, Currency.USD), RevenueType.Bonus, Created));

            func.Should().Throw<ContractException>()
                .WithMessage("It is only possible to add money to budget in its base currency: PLN");
        }

        [Fact]
        public void GivenBudgetWithRevenues_WhenGettingRevenues_ThenRevenueDetailsAreReturned()
        {
            var revenues = Budget.Revenues();
            
            revenues.Count.Should().Be(2);
            
            var partnerSalary = revenues.First();
            var salary = revenues.Last();
            
            partnerSalary.Type.Should().Be(RevenueType.PartnerSalary);
            partnerSalary.Amount.Should().Be(Amount.Create(1250.55M, Currency.PLN));
            
            salary.Type.Should().Be(RevenueType.Salary);
            salary.Amount.Should().Be(Amount.Create(1000, Currency.PLN));
        }
        
        [Fact]
        public void GivenBudgetWithExpenses_WhenGettingExpenses_ThenExpenseDetailsAreReturned()
        {
            var expenses = Budget.Expenses();
            
            expenses.Count.Should().Be(2);
            
            var beauty = expenses.First();
            var car = expenses.Last();
            
            beauty.Type.Should().Be(ExpenseType.Beauty);
            beauty.Amount.Should().Be(Amount.Create(-50.55M, Currency.PLN));
            
            car.Type.Should().Be(ExpenseType.Car);
            car.Amount.Should().Be(Amount.Create(-50.55M, Currency.PLN));
        }

        [Fact]
        public void GivenNewBudget_WhenBudgetIsPlanned_ThenItContainsExpensesAndRevenues()
        {
            var currency = Currency.USD;
            var budget = new Budget(MonthName.January, 2019, currency, Created, ImmutableList<IMoney>.Empty);
            
            budget = budget
                .WithRevenue(new Revenue(Amount.Create(10.99M, currency), RevenueType.Salary, Created))
                .WithRevenue(new Revenue(Amount.Create(0.1M, currency), RevenueType.Bonus, Created))
                .WithRevenue(new Revenue(Amount.Create(0.5M, currency), RevenueType.Rental, Created))
                .WithRevenue(new Revenue(Amount.Create(100, currency), RevenueType.Salary, Created))
                .WithExpense(new Expense(Amount.Create(-25, currency), ExpenseType.Food, Created))
                .WithExpense(new Expense(Amount.Create(-30.99M, currency), ExpenseType.Media, Created));

            new TotalBalance(budget)
                .Amount.Should().Be(Amount.Create(55.60M, currency));

            budget =
                budget.WithExpense(
                    new Expense(Amount.Create(-90, currency), ExpenseType.Car, Created));
            
            new TotalBalance(budget)
                .Amount.Should().Be(Amount.Create(-34.40M, currency));

            new TotalExpense(budget)
                .Amount.Should().Be(Amount.Create(-145.99M, currency));
            
            new TotalRevenue(budget)
                .Amount.Should().Be(Amount.Create(111.59M, currency));

            budget.Revenues().Should()
                .Contain(i => i.Type == RevenueType.Salary && i.Amount.Value == 10.99M);
            budget.Revenues().Should()
                .Contain(i => i.Type == RevenueType.Salary && i.Amount.Value == 100);
            budget.Revenues().Should()
                .Contain(i => i.Type == RevenueType.Bonus && i.Amount.Value == 0.1M);
            budget.Revenues().Should()
                .Contain(i => i.Type == RevenueType.Rental && i.Amount.Value == 0.5M);
        }

        [Fact]
        public void GivenTwoTheSameBudgets_WhenBudgetsAreCompared_ThenTheyAreEquals()
        {
            Budget.Moneys.Should().BeEquivalentTo(Budget.Moneys);
            Budget.Created.Should().Be(Budget.Created);
            Budget.BaseCurrency.Should().Be(Budget.BaseCurrency);
            Budget.Year.Should().Be(Budget.Year);
            Budget.MonthName.Should().Be(Budget.MonthName);
        }
    }
}