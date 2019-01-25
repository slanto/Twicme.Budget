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
            var budget = new Budget(Month.April, 2019, Currency.PLN, 
                    _created, ImmutableList<Money>.Empty)
                .Add(new Money(Amount.Create(-50.55M, Currency.PLN), Category.Beauty, _created))
                .Add(new Money(Amount.Create(-50.55M, Currency.PLN), Category.Car, _created))
                .Add(new Money(Amount.Create(1250.55M, Currency.PLN), Category.PartnerSalary,
                    _created))
                .Add(new Money(Amount.Create(1000, Currency.PLN), Category.Salary, _created));

            return budget;
        }
    }
}