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
            var sut = new Budget(Month.July, 2012, Currency.PLN, Created, ImmutableList<Money>.Empty);

            sut.Should().NotBeNull();
            sut.Created.Should().Be(Created);
            sut.Month.Should().Be(Month.July);
            sut.Year.Should().Be(2012);
            sut.Moneys.Should().BeEmpty();
        }

        [Fact]
        public void GivenBudget_WhenMoneyIsAdded_ThenBalanceContainsAdditionalAmount()
        {
            var Money = new Money(Amount.Create(200, Currency.PLN), Category.Bonus, Created);
            
            var budget = Budget.Add(Money);
            budget.Moneys.Should().Contain(Money);
        }
        
        [Fact]
        public void GivenBudgetInPLN_WhenAmountInUSDIsAdded_ThenExceptionIsThrown()
        {
            Func<Budget> func = () => Budget.Add(
                new Money(Amount.Create(200, Currency.USD), Category.Bonus, Created));

            func.Should().Throw<ContractException>()
                .WithMessage("It is only possible to add money to budget in its base currency: PLN");
        }

        [Fact]
        public void GivenBudgetAdds_WhenGettingMoneys_ThenMoneyDetailsAreReturned()
        {
            var moneys = Budget.Moneys;
            
            moneys.Count.Should().Be(2);
            
            var partnerSalary = moneys.First();
            var salary = moneys.Last();
            
            partnerSalary.Category.Should().Be(Category.PartnerSalary);
            partnerSalary.Amount.Should().Be(Amount.Create(1250.55M, Currency.PLN));
            
            salary.Category.Should().Be(Category.Salary);
            salary.Amount.Should().Be(Amount.Create(1000, Currency.PLN));
        }
        
//        [Fact]
//        public void GivenBudgetAdds_WhenGettingMoneys_ThenMoneyDetailsAreReturned()
//        {
//            var Moneys = Budget.Moneys();
//            
//            Moneys.Count.Should().Be(2);
//            
//            var beauty = Moneys.First();
//            var car = Moneys.Last();
//            
//            beauty.Category.Should().Be(Category.Beauty);
//            beauty.Amount.Should().Be(Amount.Create(-50.55M, Currency.PLN));
//            
//            car.Category.Should().Be(Category.Car);
//            car.Amount.Should().Be(Amount.Create(-50.55M, Currency.PLN));
//        }

        [Fact]
        public void GivenNewBudget_WhenBudgetIsPlanned_ThenItContainsMoneysAndMoneys()
        {
            var currency = Currency.USD;
            var budget = new Budget(Month.January, 2019, currency, Created, ImmutableList<Money>.Empty);
            
            budget = budget
                .Add(new Money(Amount.Create(10.99M, currency), Category.Salary, Created))
                .Add(new Money(Amount.Create(0.1M, currency), Category.Bonus, Created))
                .Add(new Money(Amount.Create(0.5M, currency), Category.Rental, Created))
                .Add(new Money(Amount.Create(100, currency), Category.Salary, Created))
                .Add(new Money(Amount.Create(-25, currency), Category.Food, Created))
                .Add(new Money(Amount.Create(-30.99M, currency), Category.Media, Created));

            new TotalBalance(budget)
                .Amount.Should().Be(Amount.Create(55.60M, currency));

            budget =
                budget.Add(
                    new Money(Amount.Create(-90, currency), Category.Car, Created));
            
            new TotalBalance(budget)
                .Amount.Should().Be(Amount.Create(-34.40M, currency));

            new TotalExpense(budget)
                .Amount.Should().Be(Amount.Create(-145.99M, currency));
            
            new TotalRevenue(budget)
                .Amount.Should().Be(Amount.Create(111.59M, currency));

            budget.Moneys.Should()
                .Contain(i => i.Category == Category.Salary && i.Amount.Value == 10.99M);
            budget.Moneys.Should()
                .Contain(i => i.Category == Category.Salary && i.Amount.Value == 100);
            budget.Moneys.Should()
                .Contain(i => i.Category == Category.Bonus && i.Amount.Value == 0.1M);
            budget.Moneys.Should()
                .Contain(i => i.Category == Category.Rental && i.Amount.Value == 0.5M);
        }

        [Fact]
        public void GivenTwoTheSameBudgets_WhenBudgetsAreCompared_ThenTheyAreEquals()
        {
            Budget.Moneys.Should().BeEquivalentTo(Budget.Moneys);
            Budget.Created.Should().Be(Budget.Created);
            Budget.BaseCurrency.Should().Be(Budget.BaseCurrency);
            Budget.Year.Should().Be(Budget.Year);
            Budget.Month.Should().Be(Budget.Month);
        }
    }
}