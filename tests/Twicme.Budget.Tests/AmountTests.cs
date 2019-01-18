using FluentAssertions;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class AmountTests
    {
        [Fact]
        public void GivenTwoAmounts_WhenTheyAreCompared_ThenTheyAreEqual()
        {
            var amount1 = Amount.Create(100.12M, Currency.PLN);
            var amount2 = Amount.Create(100.12M, Currency.PLN);
            
            amount1.Should().BeEquivalentTo(amount2);
        }
    }
}