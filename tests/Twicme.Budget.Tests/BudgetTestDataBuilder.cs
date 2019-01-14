using System;
using System.Collections.Immutable;

namespace Twicme.Budget.Tests
{
    public class BudgetTestDataBuilder
    {
        private DateTimeOffset _dateTimeOffset = DateTimeOffset.UtcNow;
        
        public BudgetTestDataBuilder WithDateTime(DateTimeOffset dateTimeOffset)
        {
            _dateTimeOffset = dateTimeOffset;
            return this;
        }
        
        public Budget Build()
        {
            var clockFake = new ClockFake(_dateTimeOffset);

            var budget = new Budget(Month.April, 2019, Currency.PLN, ImmutableList<IMoney>.Empty,
                    clockFake.NowUtc)
                .WithExpense(new Expense(Amount.Create(-50.55M, Currency.PLN), ExpenseType.Beauty, clockFake.NowUtc))
                .WithExpense(new Expense(Amount.Create(-50.55M, Currency.PLN), ExpenseType.Car, clockFake.NowUtc))
                .WithRevenue(new Revenue(Amount.Create(1250.55M, Currency.PLN), RevenueType.PartnerSalary,
                    clockFake.NowUtc))
                .WithRevenue(new Revenue(Amount.Create(1000, Currency.PLN), RevenueType.Salary, clockFake.NowUtc));

            return budget;
        }
    }
}