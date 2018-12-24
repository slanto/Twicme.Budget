using FluentAssertions;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class MoneyTests
    {
        [Fact]
        public void GivenCorrectInputData_WhenConstructorIsCalled_ThenMoneyIsCreated()
        {
            var sut = new Money(10, "PLN");

            sut.Value.Should().Be(10);
            sut.Currency.Should().Be("PLN");
        }
    }
}