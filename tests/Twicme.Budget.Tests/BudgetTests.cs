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
        [Fact]
        public void GivenCorrectInputData_WhenConstructorIsCalled_ThenBudgetIsInitialized()
        {
            var sut = new Budget(Month.July, 2012);

            sut.Should().NotBeNull();
            sut.Created.Should().BeCloseTo(DateTimeOffset.UtcNow);
            sut.Month.Should().Be(Month.July);
            sut.Year.Should().Be(2012);
            sut.Moneys.Should().BeEmpty();
        }

        [Fact]
        public void GivenBudget_WhenBalanceIsCalled_ThenCorrectAmountIsReturned()
        {
            Budget.Balance().Should().Be(Amount.Create(2149.45M, Currency.PLN));
        }

        [Fact]
        public void GivenBudget_WhenAddIsCalled_ThenAmountIsAdded()
        {
            var revenue = new Revenue(Amount.Create(200, Currency.PLN), RevenueType.Bonus);
            
            var budget = Budget.Add(revenue);
            budget.Moneys.Should().Contain(revenue);
            budget.Balance().Should().Be(Amount.Create(2349.45M, Currency.PLN));
        }

        [Fact]
        public void GivenBudgetWithRevenues_WhenGettingRevenues_ThenRevenueDetailsAreReturning()
        {
            Budget.Moneys.Count(m => m.IsRevenue()).Should().Be(2);
            
            var partnerSalary = Budget.Moneys.First(m => m.IsRevenue()).AsRevenue();
            var salary = Budget.Moneys.Last(m => m.IsRevenue()).AsRevenue();
            
            partnerSalary.Type.Should().Be(RevenueType.PartnerSalary);
            partnerSalary.Amount.Should().Be(Amount.Create(1250.55M, Currency.PLN));
            
            salary.Type.Should().Be(RevenueType.Salary);
            salary.Amount.Should().Be(Amount.Create(1000, Currency.PLN));
        }
        
        private static Budget Budget => 
            new Budget(Month.April, 2019)
                .WithExpense(new Expense(Amount.Create(-50.55M, Currency.PLN), ExpenseType.Beauty))
                .WithExpense(new Expense(Amount.Create(-50.55M, Currency.PLN), ExpenseType.Car))
                .WithRevenue(new Revenue(Amount.Create(1250.55M, Currency.PLN), RevenueType.PartnerSalary))
                .WithRevenue(new Revenue(Amount.Create(1000, Currency.PLN), RevenueType.Salary));
    }
}