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
            var sut = new Expense();

            sut.Should().NotBeNull("expense is created");
        }
    }
}