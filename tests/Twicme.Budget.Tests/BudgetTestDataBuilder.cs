using System;
using System.Collections.Immutable;

namespace Twicme.Budget.Tests
{
    public class BudgetTestDataBuilder
    {
        private DateTimeOffset _dateTime = DateTimeOffset.UtcNow;
        
        public BudgetTestDataBuilder WithDateTime(DateTime dateTime)
        {
            _dateTime = dateTime;
            return this;
        }
        
        public Budget Build()
        {
            var clockFake = new ClockFake(_dateTime);

            return new Budget(Month.April, 2019, Currency.PLN, ImmutableList<IMoney>.Empty,
                    clockFake)
                .WithExpense(new Expense(Amount.Create(-50.55M, Currency.PLN), ExpenseType.Beauty, clockFake))
                .WithExpense(new Expense(Amount.Create(-50.55M, Currency.PLN), ExpenseType.Car, clockFake))
                .WithRevenue(new Revenue(Amount.Create(1250.55M, Currency.PLN), RevenueType.PartnerSalary, clockFake))
                .WithRevenue(new Revenue(Amount.Create(1000, Currency.PLN), RevenueType.Salary, clockFake));
        }
    }

    public class ClockFake : IClock
    {
        public DateTimeOffset DateTime { get; }

        public ClockFake(DateTimeOffset dateTime)
        {
            DateTime = dateTime;
        }

        public DateTimeOffset NowUtc => DateTime;
    }
}