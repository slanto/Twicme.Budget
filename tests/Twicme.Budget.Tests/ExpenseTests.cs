using System;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class ExpenseTests
    {
        [Fact]
        public void GivenCorrectInputData_WhenConstructorIsCalled_ThenExpenseIsCreated()
        {
            var money = Money.Create(10.12M, Currency.PLN);
            const string description = "phone bill";
            
            var sut = new Expense(money, ExpenseType.Education, description);

            sut.Created.Should().BeCloseTo(DateTimeOffset.UtcNow);
            sut.Should().NotBeNull("expense is created");
            sut.Amount.Should().Be(money.Amount);
            sut.Currency.Should().Be(money.Currency);
        }
    }
}