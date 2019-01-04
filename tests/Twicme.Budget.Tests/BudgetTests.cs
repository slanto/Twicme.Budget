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
            var sut = new Budget(Month.July, 2012, ImmutableList<IMoney>.Empty);

            sut.Should().NotBeNull();
            sut.Created.Should().BeCloseTo(DateTimeOffset.UtcNow);
            sut.Month.Should().Be(Month.July);
            sut.Year.Should().Be(2012);
        }

        [Fact]
        public void GivenBudget_WhenBalanceIsCalled_ThenBalancesIsReturned()
        {
            var sut = BuildBudget();

            sut.Balance().Should().Be(Amount.Create(2149.45M, Currency.PLN));
        }

        [Fact]
        public void GivenBudget_WhenAddIsCalled_ThenAmountIsAdded()
        {
            var sut = BuildBudget();

            var revenue = new Revenue(Amount.Create(200, Currency.PLN), RevenueType.Bonus);
            
            var budget = sut.Add(revenue);
            budget.Moneys.Should().Contain(revenue);
            budget.Balance().Should().Be(Amount.Create(2349.45M, Currency.PLN));
        }

        private static Budget BuildBudget()
        {
            return new Budget(Month.April, 2019,
                ImmutableList.Create<IMoney>(
                    new Revenue(Amount.Create(1250.55M, Currency.PLN), RevenueType.PartnerSalary),
                    new Revenue(Amount.Create(1000, Currency.PLN), RevenueType.PartnerSalary),
                    new Expense(Amount.Create(-50.55M, Currency.PLN), ExpenseType.Beauty),
                    new Expense(Amount.Create(-50.55M, Currency.PLN), ExpenseType.Car)));
        }
    }
}