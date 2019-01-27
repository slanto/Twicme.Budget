using FluentAssertions;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class MonthTests
    {
        [Fact]
        public void GivenCorrectInputData_WhenInitialized_ThenMonthIsCreated()
        {
            var month = new Month(2008, MonthName.April);

            month.Value.Year.Should().Be(2008);
            month.Value.Month.Should().Be(4);
            month.Value.Day.Should().Be(1);
        }
        
        [Fact]
        public void GivenTwoObjects_WhenCompared_ThenAreEqual()
        {
            var month1 = new Month(2008, MonthName.April);
            var month2 = new Month(2008, MonthName.April);

            month1.Should().BeEquivalentTo(month2);
        }
    }
}