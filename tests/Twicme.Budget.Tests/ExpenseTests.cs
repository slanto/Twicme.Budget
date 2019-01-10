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
            var amount = Amount.Create(-10.12M, Currency.PLN);
            const string description = "phone bill";
            
            var sut = new Expense(amount, ExpenseType.Education, description);

            sut.Created.Should().BeCloseTo(DateTimeOffset.UtcNow);
            sut.Should().NotBeNull("expense is created");
            sut.Amount.Should().Be(amount);
        }
        
        [Fact]
        public void GivenPositiveAmount_WhenConstructorIsCalled_ThenExceptionIsThrown()
        {
            Func<Expense> sut = () => new Expense(Amount.Create(10, Currency.PLN), ExpenseType.Education);

            sut.Should().Throw<ContractException>()
                .WithMessage("Expense can be only negative");
        }
    }
}