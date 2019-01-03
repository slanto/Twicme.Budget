using System;
using System.Collections.Immutable;
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
            var sut = new Budget(Month.July, 2012, ImmutableList<Revenue>.Empty, ImmutableList<Revenue>.Empty, 
                ImmutableList<Expense>.Empty, ImmutableList<Expense>.Empty);

            sut.Should().NotBeNull();
            sut.Created.Should().BeCloseTo(DateTimeOffset.UtcNow);
            sut.Month.Should().Be(Month.July);
            sut.Year.Should().Be(2012);
        }

        [Fact]
        public void GivenPlanAndFactBalances_WhenConstructorIsCalled_ThenRevenueAndExpenseBalancesAreCalculated()
        {
            
            var sut = new Budget(Month.April, 2019,
                 ImmutableList.Create(new Revenue(Money.Create(1250.55M, Currency.PLN),
                    RevenueType.PartnerSalary)),
                ImmutableList.Create(new Revenue(Money.Create(1000, Currency.PLN), RevenueType.PartnerSalary)),
                ImmutableList.Create(new Expense(Money.Create(50.55M, Currency.PLN), ExpenseType.Beauty)),
                ImmutableList.Create(new Expense(Money.Create(50.55M, Currency.PLN), ExpenseType.Car)));

            sut.RevenueBalance.Should().NotBeNull();
            sut.RevenueBalance.Value.Should().BeEquivalentTo(Money.Create(-250.55M, Currency.PLN));
            sut.ExpenseBalance.Should().NotBeNull();
            sut.ExpenseBalance.Value.Should().BeEquivalentTo(Money.Create(0, Currency.PLN));
        }

        [Fact]
        public void GivenPlannedRevenue_WhenAddPlannedRevenueIsCalled_ThenRevenueIsAdded()
        {
            var sut = new Budget(Month.April, 2019,
                ImmutableList.Create(new Revenue(Money.Create(1250.55M, Currency.PLN),
                    RevenueType.PartnerSalary)),
                ImmutableList.Create(new Revenue(Money.Create(1000, Currency.PLN), RevenueType.PartnerSalary)),
                ImmutableList.Create(new Expense(Money.Create(50.55M, Currency.PLN), ExpenseType.Beauty)),
                ImmutableList.Create(new Expense(Money.Create(50.55M, Currency.PLN), ExpenseType.Car)));

            var newRevenue = new Revenue(Money.Create(200, Currency.PLN), RevenueType.Bonus);
            
            var newBudget = sut.AddPlannedRevenue(newRevenue);
            newBudget.PlannedRevenues.Should().Contain(newRevenue);
        }
    }
}