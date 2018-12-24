using System;
using FluentAssertions;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class ExpenseTests
    {
        [Fact]
        public void GivenCorrectInputData_WhenConstructorIsCalled_ThenExpenseIsCreated()
        {
            var sut = new Expense(DateTimeOffset.UtcNow, Money.CreateZloty(10.12M), "phone bill");

            sut.Should().NotBeNull("expense is created");
        }
    }
}