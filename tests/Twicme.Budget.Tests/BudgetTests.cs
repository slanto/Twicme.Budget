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
            var sut = new Budget(Month.Create(2012, MonthName.July), 
                Currency.PLN, Created, ImmutableList<Money>.Empty);

            sut.Should().NotBeNull();
            sut.CreatedOn.Should().Be(Created);
            sut.Month.Should().Be(Month.Create(2012, MonthName.July));
            sut.Moneys.Should().BeEmpty();
        }

        [Fact]
        public void GivenBudget_WhenMoneyIsAdded_ThenBudgetContainsAdditionalAmount()
        {
            var money = new Money(Amount.Create(200, Currency.PLN), Category.Salary, Created);
            
            var budget = Budget.WithRevenue(money);
            budget.Moneys.Should().Contain(money);
        }
        
        [Fact]
        public void GivenBudgetInPLN_WhenAmountInUSDIsAdded_ThenExceptionIsThrown()
        {
            Func<Budget> func = () => Budget.WithRevenue(
                new Money(Amount.Create(200, Currency.USD), Category.Salary, Created));

            func.Should().Throw<ContractException>()
                .WithMessage("It is only possible to add money to budget in its base currency: PLN");
        }

        [Fact]
        public void GivenBudget_WhenGettingMoneys_ThenMoneyDetailsAreReturned()
        {
            var moneys = Budget.Moneys;
            
            moneys.Count.Should().Be(4);

            moneys.Should().Contain(m => m.Category == Category.OtherIncome);
            moneys.Should().Contain(m => m.Amount == Amount.Create(1250.55M, Currency.PLN));
            
            moneys.Should().Contain(m => m.Category == Category.Salary);
            moneys.Should().Contain(m => m.Amount == Amount.Create(1000, Currency.PLN));
            
            moneys.Should().Contain(m => m.Category == Category.CarAndTransport);
            moneys.Should().Contain(m => m.Amount == Amount.Create(-50.55M, Currency.PLN));
            
            moneys.Should().Contain(m => m.Category == Category.HomeAndBills);
            moneys.Should().Contain(m => m.Amount == Amount.Create(-50.55M, Currency.PLN));
        }
    
        [Fact]
        public void GivenNewBudget_WhenBudgetIsPlanned_ThenItContainsMoneys()
        {
            var currency = Currency.USD;
            var budget = new Budget(Month.Create(2019, MonthName.January), currency, Created, ImmutableList<Money>.Empty);
            
            budget = budget
                .WithRevenue(new Money(Amount.Create(10.99M, currency), Category.Salary, Created))
                .WithRevenue(new Money(Amount.Create(0.1M, currency), Category.Salary, Created))
                .WithRevenue(new Money(Amount.Create(0.5M, currency), Category.Salary, Created))
                .WithRevenue(new Money(Amount.Create(100, currency), Category.OtherIncome, Created))
                .WithExpense(new Money(Amount.Create(-25, currency), Category.BasicExpenditure, Created))
                .WithExpense(new Money(Amount.Create(-30.99M, currency), Category.HomeAndBills, Created));

            new TotalBalance(budget)
                .Amount.Should().Be(Amount.Create(55.60M, currency));

            budget =
                budget.WithExpense(
                    new Money(Amount.Create(-90, currency), Category.CarAndTransport, Created));
            
            new TotalBalance(budget)
                .Amount.Should().Be(Amount.Create(-34.40M, currency));

            new TotalExpense(budget)
                .Amount.Should().Be(Amount.Create(-145.99M, currency));
            
            new TotalRevenue(budget)
                .Amount.Should().Be(Amount.Create(111.59M, currency));

            budget.Moneys.Should()
                .Contain(i => i.Category == Category.Salary && i.Amount.Value == 10.99M);
            budget.Moneys.Should()
                .Contain(i => i.Category == Category.OtherIncome && i.Amount.Value == 100);
            budget.Moneys.Should()
                .Contain(i => i.Category == Category.Salary && i.Amount.Value == 0.1M);
            budget.Moneys.Should()
                .Contain(i => i.Category == Category.Salary && i.Amount.Value == 0.5M);
        }

        [Fact]
        public void GivenTwoTheSameBudgets_WhenBudgetsAreCompared_ThenTheyAreEquals()
        {
            Budget.Moneys.Should().BeEquivalentTo(Budget.Moneys);
            Budget.CreatedOn.Should().Be(Budget.CreatedOn);
            Budget.BaseCurrency.Should().Be(Budget.BaseCurrency);
            Budget.Month.Should().Be(Budget.Month);
        }
        
        [Fact]
        public void GivenPositiveMoney_WhenExpenseIsAdded_ThenExceptionIsThrown()
        {
            var currency = Currency.USD;
            var budget = new Budget(Month.Create(2019, MonthName.January), currency, Created, ImmutableList<Money>.Empty);
            
            Func<Budget> sut = () => budget.WithExpense(
                new Money(Amount.Create(10, currency), Category.Education, Created));

            sut.Should().Throw<ContractException>()
                .WithMessage("Expense can have only negative amount");
        }
        
        [Fact]
        public void GivenNegativeMoney_WhenRevenueIsAdded_ThenExceptionIsThrown()
        {
            var currency = Currency.USD;
            var budget = new Budget(Month.Create(2019, MonthName.January), currency, Created, ImmutableList<Money>.Empty);
            
            Func<Budget> sut = () => budget.WithRevenue(
                new Money(Amount.Create(-10, currency), Category.Salary, Created));

            sut.Should().Throw<ContractException>()
                .WithMessage("Revenue can have only positive amount");
        }

        [Fact]
        public void GivenBudget_WhenBudgetIsSearched_ThenFilteredMoneysAreReturned()
        {
            var currency = Currency.PLN;
            
            var budget = new Budget(Month.Create(2019, MonthName.January), currency, Created,
                ImmutableList<Money>.Empty);

        }
    }
}