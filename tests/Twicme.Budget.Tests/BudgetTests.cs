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
            var sut = new Budget(Month.July, 2012, MoneyCollection<Revenue>.Empty, new MoneyCollection<Revenue>(), 
                MoneyCollection<Expense>.Empty, MoneyCollection<Expense>.Empty);

            sut.Should().NotBeNull();
            sut.Created.Should().BeCloseTo(DateTimeOffset.UtcNow);
            sut.Month.Should().Be(Month.July);
            sut.Year.Should().Be(2012);
        }

        [Fact]
        public void GivenPlanAndFactBalances_WhenConstructorIsCalled_ThenRevenueAndExpenseBalancesAreCalculated()
        {
            var sut = new Budget(Month.April, 2019,
                new MoneyCollection<Revenue>(new Revenue(Money.Create(1250.55M, Currency.PLN),
                    RevenueType.PartnerSalary)),
                new MoneyCollection<Revenue>(new Revenue(Money.Create(1000, Currency.PLN), RevenueType.PartnerSalary)),
                new MoneyCollection<Expense>(new Expense(Money.Create(50.55M, Currency.PLN), ExpenseType.Beauty)),
                new MoneyCollection<Expense>(new Expense(Money.Create(50.55M, Currency.PLN), ExpenseType.Car)));

            sut.RevenueBalance.Should().NotBeNull();
            sut.RevenueBalance.Value.Should().BeEquivalentTo(Money.Create(-250.55M, Currency.PLN));
            sut.ExpenseBalance.Should().NotBeNull();
            sut.ExpenseBalance.Value.Should().BeEquivalentTo(Money.Create(0, Currency.PLN));
        }

        [Fact]
        public void GivenPlannedRevenue_WhenAddPlannedRevenueIsCalled_ThenRevenueIsAdded()
        {
            var sut = new Budget(Month.April, 2019,
                new MoneyCollection<Revenue>(new Revenue(Money.Create(1250.55M, Currency.PLN),
                    RevenueType.PartnerSalary)),
                new MoneyCollection<Revenue>(new Revenue(Money.Create(1000, Currency.PLN), RevenueType.PartnerSalary)),
                new MoneyCollection<Expense>(new Expense(Money.Create(50.55M, Currency.PLN), ExpenseType.Beauty)),
                new MoneyCollection<Expense>(new Expense(Money.Create(50.55M, Currency.PLN), ExpenseType.Car)));

            var newRevenue = new Revenue(Money.Create(200, Currency.PLN), RevenueType.Bonus);
            sut.AddPlannedRevenue(newRevenue);

            sut.PlannedRevenues.Should().Contain(newRevenue);
        }
    }
}