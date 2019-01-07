using System;
using System.Collections.Immutable;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class BudgetTests
    {   
        private static Budget Budget => new BudgetTestDataBuilder().Build();
        
        [Fact]
        public void GivenCorrectInputData_WhenConstructorIsCalled_ThenBudgetIsInitialized()
        {
            var sut = new Budget(Month.July, 2012, Currency.PLN);

            sut.Should().NotBeNull();
            sut.Created.Should().BeCloseTo(DateTimeOffset.UtcNow);
            sut.Month.Should().Be(Month.July);
            sut.Year.Should().Be(2012);
            sut.Moneys.Should().BeEmpty();
        }

        [Fact]
        public void GivenBudget_WhenMoneyIsAdded_ThenBalanceContainsAdditionalAmount()
        {
            var revenue = new Revenue(Amount.Create(200, Currency.PLN), RevenueType.Bonus);
            
            var budget = Budget.Add(revenue);
            budget.Moneys.Should().Contain(revenue);
        }
        
        [Fact]
        public void GivenBudgetInPLN_WhenAmountInUSDIsAdded_ThenExceptionIsThrown()
        {
            Func<Budget> func = () => Budget.Add(
                new Revenue(Amount.Create(200, Currency.USD), RevenueType.Bonus));

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
    }
}