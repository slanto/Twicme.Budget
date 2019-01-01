using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class BalanceTests
    {
        [Theory]
        [MemberData(nameof(BalanceTestCases))]
        public void GivenTwoValues_WhenValueIsCalled_BalanceIsCalculated(decimal value1, decimal value2, decimal balance)
        {
            var sut = new Balance(Money.Create(value1, Currency.PLN), Money.Create(value2, Currency.PLN));

            sut.Value.Should().Be(Money.Create(balance, Currency.PLN));
        }
        
        public static IEnumerable<object[]> BalanceTestCases()
        {
            yield return new object[]
            {
                220, 22.51M, 197.49M
            };
            
            yield return new object[]
            {
                100.55M, 110, -9.45M
            };
        }
    }
}