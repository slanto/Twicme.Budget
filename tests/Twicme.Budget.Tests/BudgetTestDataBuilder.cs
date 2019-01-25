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
            var budget = new Budget(MonthName.April, 2019, Currency.PLN, 
                    _created, ImmutableList<IMoney>.Empty)
                .WithExpense(new Expense(Amount.Create(-50.55M, Currency.PLN), ExpenseType.Beauty, _created))
                .WithExpense(new Expense(Amount.Create(-50.55M, Currency.PLN), ExpenseType.Car, _created))
                .WithRevenue(new Revenue(Amount.Create(1250.55M, Currency.PLN), RevenueType.PartnerSalary,
                    _created))
                .WithRevenue(new Revenue(Amount.Create(1000, Currency.PLN), RevenueType.Salary, _created));

            return budget;
        }
    }
}