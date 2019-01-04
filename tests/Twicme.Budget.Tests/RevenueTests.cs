using System;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class RevenueTests
    {               
        [Fact]
        public void GivenCorrectInputData_WhenConstructorIsCalled_ThenRevenueIsCreated()
        {
            var money = Amount.Create(10.12M, Currency.PLN);
            const string description = "my salary";
            
            var sut = new Revenue(money, RevenueType.Salary, description);

            sut.Should().NotBeNull("revenue is created");
            sut.Created.Should().BeCloseTo(DateTimeOffset.UtcNow);
            sut.Value.Should().Be(money.Value);
            sut.Currency.Should().Be(money.Currency);
            sut.Type.Should().Be(RevenueType.Salary);
            sut.Description.Should().Be(description);
        }
    }
}