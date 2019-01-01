using System;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class BudgetTests
    {   
        [Fact]
        public void GivenCorrectInputData_WhenConstructorIsCalled_ThenBudgetIsInitialized()
        {
            var sut = new Budget(Month.July, 2012, new Revenue[0], new Revenue[0], 
                new Expense[0], new Expense[0]);

            sut.Should().NotBeNull();
            sut.Created.Should().BeCloseTo(DateTimeOffset.UtcNow);
            sut.Month.Should().Be(Month.July);
            sut.Year.Should().Be(2012);
        }

        [Fact]
        public void GivenPlanAndFactBalances_WhenConstructorIsCalled_ThenRevenueAndExpenseBalancesAreCalculated()
        {
            var sut = new Budget(Month.April, 2019, 
                new[] {new Revenue(Money.Create(1250.55M, Currency.PLN), RevenueType.PartnerSalary)},
                new[] {new Revenue(Money.Create(1000, Currency.PLN), RevenueType.PartnerSalary)},
                new[] {new Expense(Money.Create(50.55M, Currency.PLN), ExpenseType.Beauty)},
                new[] {new Expense(Money.Create(50.55M, Currency.PLN), ExpenseType.Car)});
                
            sut.RevenueBalance.Should().BeEquivalentTo(Money.Create(-250.55M, Currency.PLN));
            sut.ExpenseBalance.Should().BeEquivalentTo(Money.Create(0, Currency.PLN));
        }
    }
}