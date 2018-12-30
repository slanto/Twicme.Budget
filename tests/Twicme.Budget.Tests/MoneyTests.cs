using System;
using System.Collections.Generic;
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
            sut.Currency.Should().Be(Money.PLNCurrency);
        }

        [Fact]
        public void GivenValueWithHighPrecision_WhenConstructorIsCalled_ThenMoneyIsCreatedWithTwoDecimalPlace()
        {
            var sut = Money.CreateZloty(0.2678765678M);

            sut.Amount.Should().Be(0.27M);
        }
        
        public static IEnumerable<object[]> PlusOperatorTestCases()
        {
            yield return new object[]
            {
                Money.CreateZloty(12),
                Money.CreateZloty(8),
                Money.CreateZloty(20)
            };
            
            yield return new object[]
            {
                Money.CreateZloty(0),
                Money.CreateZloty(1433),
                Money.CreateZloty(1433)
            };
            
            yield return new object[]
            {
                Money.CreateZloty(0.25M),
                Money.CreateZloty(0.123456789M),
                Money.CreateZloty(0.37M)
            };
            
            yield return new object[]
            {
                Money.CreateZloty(0.99M),
                Money.CreateZloty(1000),
                Money.CreateZloty(1000.99M)
            };
        }
            
        [Theory, MemberData(nameof(PlusOperatorTestCases))]
        public void GivenElements_WhenPlusOperatorIsCalled_ThenSumIsCalculated(Money elem1, Money elem2, Money sum)
        {
            var result = elem1 + elem2;
            
            result.Should().BeEquivalentTo(sum);
        }

        [Fact]
        public void GivenElementsWithDifferentCurrency_WhenSumIsCalculated_ThenExceptionIsThrown()
        {
            var zloty = Money.CreateZloty(1);
            var dollar = Money.CreateDollar(1);
            
            Func<Money> act = () => zloty + dollar;

            act.Should().Throw<ContractException>()
                .WithMessage("It is only possible to add money with the same currency");
        }
    }
}