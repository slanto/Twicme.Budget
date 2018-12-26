using System;
using FluentAssertions;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class RevenueTests
    {
        [Fact]
        public void GivenCorrectInputData_WhenConstructorIsCalled_ThenRevenueIsCreated()
        {
            var sut = new Revenue(Money.CreateZloty(10.12M), RevenueType.Salary, "my salary");

            sut.Should().NotBeNull("revenue is created");
        }
    }
}