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
                new[] {new Revenue(Money.CreateZloty(1250.55M), RevenueType.PartnerSalary, string.Empty)},
                new[] {new Revenue(Money.CreateZloty(1000), RevenueType.PartnerSalary, string.Empty)},
                new[] {new Expense(Money.CreateZloty(50.55M), string.Empty)},
                new[] {new Expense(Money.CreateZloty(50.55M), string.Empty)});
                
            sut.RevenueBalance.Should().BeEquivalentTo(Money.CreateZloty(-250.55M));
            sut.ExpenseBalance.Should().BeEquivalentTo(Money.CreateZloty(0));
        }
    }
}