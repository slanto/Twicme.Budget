using System;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class RevenueTests
    {
        private readonly IClock _clock;
        private static DateTimeOffset Now => new DateTimeOffset(2019, 1, 1, 10, 20, 0, TimeSpan.Zero);
        
        public RevenueTests()
        {
            _clock = Substitute.For<IClock>();
            _clock.UtcNow.Returns(Now);
        }
        
        [Fact]
        public void GivenCorrectInputData_WhenConstructorIsCalled_ThenRevenueIsCreated()
        {
            var money = Money.CreateZloty(10.12M);
            const string description = "my salary";
            
            var sut = new Revenue(money, RevenueType.Salary, _clock, description);

            sut.Should().NotBeNull("revenue is created");
            sut.Created.Should().Be(Now);
            sut.Money.Should().BeEquivalentTo(money);
            sut.Type.Should().Be(RevenueType.Salary);
            sut.Description.Should().Be(description);
        }
    }
}