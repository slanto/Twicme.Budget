using FluentAssertions;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class ExpenseTypeTests
    {
        [Fact]
        public void GivenTwoExpenseTypes_WhenTheyAreCompared_ThenTheyAreEqual()
        {
            var type1 = ExpenseType.Create("expense-type");
            var type2 = ExpenseType.Create("expense-type");
            
            type1.Should().BeEquivalentTo(type2);
        }
    }
}