using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class MoneyTests
    {
        private static DateTimeOffset Created => 
            new DateTimeOffset(2018, 2, 10, 6, 12, 0, TimeSpan.Zero);
        
        [Fact]
        public void GivenCorrectInputData_WhenConstructorIsCalled_ThenMoneyIsCreated()
        {
            var sut = Amount.Create(10, Currency.PLN);

            sut.Value.Should().Be(10);
            sut.Currency.Should().Be(Currency.PLN);
        }

        [Fact]
        public void GivenValueWithHighPrecision_WhenConstructorIsCalled_ThenMoneyIsCreatedWithTwoDecimalPlace()
        {
            var sut = Amount.Create(0.2678765678M, Currency.PLN);

            sut.Value.Should().Be(0.27M);
        }
        
        public static IEnumerable<object[]> PlusOperatorTestCases()
        {
            yield return new object[]
            {
                Amount.Create(12, Currency.PLN),
                Amount.Create(8, Currency.PLN),
                Amount.Create(20, Currency.PLN)
            };
            
            yield return new object[]
            {
                Amount.Create(0, Currency.PLN),
                Amount.Create(1433, Currency.PLN),
                Amount.Create(1433, Currency.PLN)
            };
            
            yield return new object[]
            {
                Amount.Create(0.25M, Currency.PLN),
                Amount.Create(0.123456789M, Currency.PLN),
                Amount.Create(0.37M, Currency.PLN)
            };
            
            yield return new object[]
            {
                Amount.Create(0.99M, Currency.PLN),
                Amount.Create(1000, Currency.PLN),
                Amount.Create(1000.99M, Currency.PLN)
            };
        }
            
        [Theory, MemberData(nameof(PlusOperatorTestCases))]
        public void GivenElements_WhenPlusOperatorIsCalled_ThenSumIsCalculated(Amount elem1, Amount elem2, Amount sum)
        {
            var result = elem1 + elem2;
            
            result.Should().BeEquivalentTo(sum);
        }

        [Fact]
        public void GivenElementsWithDifferentCurrency_WhenSumIsCalculated_ThenExceptionIsThrown()
        {
            var zloty = Amount.Create(1, Currency.PLN);
            var dollar = Amount.Create(1, Currency.USD);
            
            Func<Amount> act = () => zloty + dollar;

            act.Should().Throw<ContractException>()
                .WithMessage("It is only possible to add money with the same currency");
        }
        
        public static IEnumerable<object[]> MinusOperatorTestCases()
        {
            yield return new object[]
            {
                Amount.Create(12, Currency.PLN),
                Amount.Create(8, Currency.PLN),
                Amount.Create(4, Currency.PLN)
            };
            
            yield return new object[]
            {
                Amount.Create(0, Currency.PLN),
                Amount.Create(1433, Currency.PLN),
                Amount.Create(-1433, Currency.PLN)
            };
            
            yield return new object[]
            {
                Amount.Create(0.25M, Currency.PLN),
                Amount.Create(0.123456789M, Currency.PLN),
                Amount.Create(0.13M, Currency.PLN)
            };
            
            yield return new object[]
            {
                Amount.Create(1000, Currency.PLN),
                Amount.Create(0.99M, Currency.PLN),
                Amount.Create(999.01M, Currency.PLN)
            };
        }
            
        [Theory, MemberData(nameof(MinusOperatorTestCases))]
        public void GivenElements_WhenMinusOperatorIsCalled_ThenDiffIsCalculated(Amount elem1, Amount elem2, Amount diff)
        {
            var result = elem1 - elem2;
            
            result.Should().BeEquivalentTo(diff);
        }

        [Fact]
        public void GivenTwoMoneys_WhenTheyAreCompared_ThenTheyAreEqual()
        {
            var money1 = new Money(Amount.Create(-100.12M, Currency.PLN),  
                Category.Car, Created, "description");
            
            var money2 = new Money(Amount.Create(-100.12M, Currency.PLN), 
                Category.Car, Created, "description");
            
            money1.Should().BeEquivalentTo(money2);
        }
    }
}