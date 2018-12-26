using FluentAssertions;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class MoneyTests
    {
        [Fact]
        public void GivenCorrectInputData_WhenConstructorIsCalled_ThenMoneyIsCreated()
        {
            var sut = Money.CreateZloty(10);

            sut.Amount.Should().Be(10);
            sut.Currency.Should().Be("PLN");
        }
    }
}