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
            var money = Money.CreateZloty(10.12M);
            const string description = "phone bill";
            
            var sut = new Expense(money, description);

            sut.Created.Should().BeCloseTo(DateTimeOffset.UtcNow);
            sut.Should().NotBeNull("expense is created");
            sut.Money.Should().BeEquivalentTo(money);
        }
    }
}