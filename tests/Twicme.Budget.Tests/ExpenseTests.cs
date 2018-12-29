using System;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class ExpenseTests
    {
        private readonly IClock _clock;
        private static DateTimeOffset Now => new DateTimeOffset(2019, 1, 1, 10, 20, 0, TimeSpan.Zero);
        
        public ExpenseTests()
        {
            _clock = Substitute.For<IClock>();
            _clock.UtcNow.Returns(Now);
        }
        
        [Fact]
        public void GivenCorrectInputData_WhenConstructorIsCalled_ThenExpenseIsCreated()
        {
            var money = Money.CreateZloty(10.12M);
            const string description = "phone bill";
            
            var sut = new Expense(money, _clock, description);

            sut.Should().NotBeNull("expense is created");
            sut.Money.Should().BeEquivalentTo(money);
            sut.Created.Should().Be(Now);
        }
    }
}