using FluentAssertions;
using Twicme.Budget.Store;
using Xunit;

namespace Twicme.Budget.Tests
{

    public class JsonBudgetTests
    {
        [Fact]
        public void GivenBudget_WhenToJsonIsCalled_ThenBudgetInJsonFormatIsReturned()
        {
            var budget = BudgetTestDataBuilder.Build();

            var sut = new JsonBudget(budget);

            var serializedBudget = sut.ToJson();

            var deserializedBudget = JsonBudget.ToBudget(serializedBudget);
            
            deserializedBudget.Should().BeEquivalentTo(budget);
        }
    }
}