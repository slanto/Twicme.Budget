using System;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class BudgetTests
    {
        private readonly IClock _clock;
        private static DateTimeOffset Now => new DateTimeOffset(2019, 1, 1, 10, 20, 0, TimeSpan.Zero);
        
        public BudgetTests()
        {
            _clock = Substitute.For<IClock>();
            _clock.UtcNow.Returns(Now);
        }
        
        [Fact]
        public void GivenCorrectInputData_WhenConstructorIsCalled_ThenBudgetIsInitialized()
        {
            var sut = new Budget(_clock, Month.July, 2012);

            sut.Should().NotBeNull();
            sut.Created.Should().Be(Now);
            sut.Month.Should().Be(Month.July);
            sut.Year.Should().Be(2012);
            
            sut.Plan.Should().NotBeNull();
            sut.Fact.Should().NotBeNull();
        }
    }
}