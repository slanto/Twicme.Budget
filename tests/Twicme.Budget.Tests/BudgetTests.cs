using System;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class BudgetTests
    {
        private readonly IClock _clock;
        private static DateTimeOffset Now => new DateTimeOffset(2019, 1, 1, 10, 20, 0, TimeSpan.Zero);
        
        public BudgetTests()
        {
            _clock = Substitute.For<IClock>();
            _clock.UtcNow.Returns(Now);
        }
        
        [Fact]
        public void GivenCorrectInputData_WhenConstructorIsCalled_ThenBudgetIsInitialized()
        {
            var sut = new Budget(_clock, Month.July, 2012, new Revenue[0], new Revenue[0], new Expense[0],
                new Expense[0]);

            sut.Should().NotBeNull();
            sut.Created.Should().Be(Now);
            sut.Month.Should().Be(Month.July);
            sut.Year.Should().Be(2012);
        }

        [Fact]
        public void GivenPlanAndFactBalances_WhenConstructorIsCalled_ThenRevenueAndExpenseBalancesAreCalculated()
        {
            var sut = new Budget(_clock, Month.April, 2019, 
                new[] {new Revenue(Money.CreateZloty(1250.55M), RevenueType.PartnerSalary)},
                new[] {new Revenue(Money.CreateZloty(1000), RevenueType.PartnerSalary)},
                new[] {new Expense(Money.CreateZloty(50.55M))},
                new[] {new Expense(Money.CreateZloty(50.55M))});
                
            sut.RevenueBalance.Should().BeEquivalentTo(Money.CreateZloty(-250.55M));
            sut.ExpenseBalance.Should().BeEquivalentTo(Money.CreateZloty(0));
        }
    }
}