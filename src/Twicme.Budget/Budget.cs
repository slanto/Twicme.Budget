using System;

namespace Twicme.Budget
{
    public class Budget
    {
        public Month Month { get; }
        public uint Year { get; }
        public DateTimeOffset Created { get; }

        private readonly Balance _plan;
        private readonly Balance _fact;
        
        public Money RevenueBalance => Money.CreateZloty(_fact.TotalRevenue.Amount - _plan.TotalRevenue.Amount);
        public Money ExpenseBalance => Money.CreateZloty(_fact.TotalExpense.Amount - _plan.TotalExpense.Amount);

        public Budget(IClock clock, Month month, uint year, Balance plan, Balance fact)
        {
            Month = month;
            Year = year;
            Created = clock.UtcNow;
            _plan = plan;
            _fact = fact;
        }
        
        public Budget(IClock clock, Month month, uint year) : this(clock, month, year, new Balance(), new Balance())
        {
        } 
        
        public Budget(Month month, uint year) : this(new Clock(), month, year, new Balance(), new Balance())
        {
        }
    }
}