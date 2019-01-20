using System;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class ExpenseTests
    {
        private static DateTimeOffset Created => 
            new DateTimeOffset(2018, 2, 10, 6, 12, 0, TimeSpan.Zero);
        
        [Fact]
        public void GivenCorrectInputData_WhenConstructorIsCalled_ThenExpenseIsCreated()
        {
            var amount = Amount.Create(-10.12M, Currency.PLN);
            const string description = "phone bill";
            
            var sut = new Expense(amount, ExpenseType.Education, Created, 
                description);

            sut.Created.Should().Be(Created);
            sut.Should().NotBeNull("expense is created");
            sut.Amount.Should().Be(amount);
        }
        
        [Fact]
        public void GivenPositiveAmount_WhenConstructorIsCalled_ThenExceptionIsThrown()
        {
            Func<Expense> sut = () => new Expense(Amount.Create(10, Currency.PLN), ExpenseType.Education, Created);

            sut.Should().Throw<ContractException>()
                .WithMessage("Expense can be only negative");
        }
    }
}