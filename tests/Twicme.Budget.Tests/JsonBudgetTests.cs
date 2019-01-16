using System;
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
            var budget = new BudgetTestDataBuilder()
                .WithDateTime(new DateTime(2019, 1, 1, 10, 10, 10))
                .Build();

            var sut = new JsonBudget(budget);

            var serializedBudget = sut.Serialize();

            var deserializedBudget = JsonBudget.Deserialize(serializedBudget);
            
            deserializedBudget.Should().BeEquivalentTo(budget);
        }
    }
}