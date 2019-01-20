using System;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class RevenueTests
    {               
        private static DateTimeOffset Created => 
            new DateTimeOffset(2018, 2, 10, 6, 12, 0, TimeSpan.Zero);

        [Fact]
        public void GivenCorrectInputData_WhenConstructorIsCalled_ThenRevenueIsCreated()
        {
            var amount = Amount.Create(10.12M, Currency.PLN);
            const string description = "my salary";
            
            var sut = new Revenue(amount, RevenueType.Salary, Created, description);

            sut.Should().NotBeNull("revenue is created");
            sut.Created.Should().Be(Created);
            sut.Amount.Should().Be(amount);
            sut.Type.Should().Be(RevenueType.Salary);
            sut.Description.Should().Be(description);
        }

        [Fact]
        public void GivenTwoRevenues_WhenTheyAreCompared_ThenTheyAreEqual()
        {
            var revenue1 = new Revenue(Amount.Create(100.12M, Currency.PLN),  
                RevenueType.Salary, Created, "description");
            var revenue2 = new Revenue(Amount.Create(100.12M, Currency.PLN), 
                RevenueType.Salary, Created, "description");
            
            revenue1.Should().BeEquivalentTo(revenue2);
        }
    }
}