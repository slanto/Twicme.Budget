using FluentAssertions;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class MoneyCollectionTests
    {
        [Fact]
        public void GivenMoneyInTheSameCurrency_WhenSumIsCalled_ThenSumResultIsCalculated()
        {
            var sut = new MoneyCollection<Money>(Money.Create(100.22M, Currency.PLN),
                Money.Create(99.99M, Currency.PLN));

            var result = sut.Sum();

            result.Should().Be(Money.Create(200.21M, Currency.PLN));
        }
    }
}