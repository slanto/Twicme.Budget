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
            var sut = Money.Create(10, Currency.PLN);

            sut.Amount.Should().Be(10);
            sut.Currency.Should().Be(Currency.PLN);
        }

        [Fact]
        public void GivenValueWithHighPrecision_WhenConstructorIsCalled_ThenMoneyIsCreatedWithTwoDecimalPlace()
        {
            var sut = Money.Create(0.2678765678M, Currency.PLN);

            sut.Amount.Should().Be(0.27M);
        }
        
        public static IEnumerable<object[]> PlusOperatorTestCases()
        {
            yield return new object[]
            {
                Money.Create(12, Currency.PLN),
                Money.Create(8, Currency.PLN),
                Money.Create(20, Currency.PLN)
            };
            
            yield return new object[]
            {
                Money.Create(0, Currency.PLN),
                Money.Create(1433, Currency.PLN),
                Money.Create(1433, Currency.PLN)
            };
            
            yield return new object[]
            {
                Money.Create(0.25M, Currency.PLN),
                Money.Create(0.123456789M, Currency.PLN),
                Money.Create(0.37M, Currency.PLN)
            };
            
            yield return new object[]
            {
                Money.Create(0.99M, Currency.PLN),
                Money.Create(1000, Currency.PLN),
                Money.Create(1000.99M, Currency.PLN)
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
            var zloty = Money.Create(1, Currency.PLN);
            var dollar = Money.Create(1, Currency.USD);
            
            Func<Money> act = () => zloty + dollar;

            act.Should().Throw<ContractException>()
                .WithMessage("It is only possible to add money with the same currency");
        }
        
        public static IEnumerable<object[]> MinusOperatorTestCases()
        {
            yield return new object[]
            {
                Money.Create(12, Currency.PLN),
                Money.Create(8, Currency.PLN),
                Money.Create(4, Currency.PLN)
            };
            
            yield return new object[]
            {
                Money.Create(0, Currency.PLN),
                Money.Create(1433, Currency.PLN),
                Money.Create(-1433, Currency.PLN)
            };
            
            yield return new object[]
            {
                Money.Create(0.25M, Currency.PLN),
                Money.Create(0.123456789M, Currency.PLN),
                Money.Create(0.13M, Currency.PLN)
            };
            
            yield return new object[]
            {
                Money.Create(1000, Currency.PLN),
                Money.Create(0.99M, Currency.PLN),
                Money.Create(999.01M, Currency.PLN)
            };
        }
            
        [Theory, MemberData(nameof(MinusOperatorTestCases))]
        public void GivenElements_WhenMinusOperatorIsCalled_ThenDiffIsCalculated(Money elem1, Money elem2, Money diff)
        {
            var result = elem1 - elem2;
            
            result.Should().BeEquivalentTo(diff);
        }

    }
}