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
            var money = Amount.Create(10.12M, Currency.PLN);
            const string description = "phone bill";
            
            var sut = new Expense(money, ExpenseType.Education, description);

            sut.Created.Should().BeCloseTo(DateTimeOffset.UtcNow);
            sut.Should().NotBeNull("expense is created");
            sut.Value.Should().Be(money.Value);
            sut.Currency.Should().Be(money.Currency);
        }
    }
}