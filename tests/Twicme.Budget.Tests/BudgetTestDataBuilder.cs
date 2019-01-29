using System;
using System.Collections.Immutable;

namespace Twicme.Budget.Tests
{
    public class BudgetTestDataBuilder
    {
        private DateTimeOffset _created = DateTimeOffset.UtcNow;
        
        public BudgetTestDataBuilder WithCreated(DateTimeOffset created)
        {
            _created = created;
            return this;
        }
        
        public Budget Build()
        {
            var budget = new Budget(Month.Create(2019, MonthName.April), Currency.PLN, 
                    _created, ImmutableList<Money>.Empty)
                .WithExpense(new Money(Amount.Create(-50.55M, Currency.PLN), Category.HomeAndBills, _created))
                .WithExpense(new Money(Amount.Create(-50.55M, Currency.PLN), Category.CarAndTransport, _created))
                .WithRevenue(new Money(Amount.Create(1250.55M, Currency.PLN), Category.OtherIncome,
                    _created))
                .WithRevenue(new Money(Amount.Create(1000, Currency.PLN), Category.Salary, _created));

            return budget;
        }
    }
}