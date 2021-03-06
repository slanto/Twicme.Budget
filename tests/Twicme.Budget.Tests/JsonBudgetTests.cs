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
                .WithCreatedOn(new DateTime(2019, 1, 1, 10, 10, 10))
                .Build();

            var jsonBudget = new JsonBudget(budget);

            var jsonContent = jsonBudget.Content;
            var deserializedBudget = jsonContent.ToBudget();
            
            deserializedBudget.Moneys.Should().BeEquivalentTo(budget.Moneys);
            deserializedBudget.CreatedOn.Should().Be(budget.CreatedOn);
            deserializedBudget.BaseCurrency.Should().Be(budget.BaseCurrency);
            deserializedBudget.Month.Should().Be(budget.Month);
        }
    }
}