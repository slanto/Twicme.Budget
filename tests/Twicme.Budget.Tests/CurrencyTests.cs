using FluentAssertions;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class CurrencyTests
    {
        [Theory]
        [InlineData("pln", "PLN")]
        [InlineData("pLN", "PLN")]
        [InlineData("PLN", "PLN")]
        [InlineData("usd", "USD")]
        [InlineData("uSD", "USD")]
        [InlineData("USD", "USD")]
        public void GivenLowercaseCurrencySymbol_WhenCurrencyIsInitialized_ThenCurrencyIsCreated
            (string inputSymbol, string expectedSymbol)
        {
            var sut = Currency.Create(inputSymbol);

            sut.Should().NotBeNull();
            sut.Symbol.Should().Be(expectedSymbol);
        }
    }
}