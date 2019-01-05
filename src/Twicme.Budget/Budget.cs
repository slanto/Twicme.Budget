using System;
using System.Collections.Immutable;
using System.Linq;

namespace Twicme.Budget
{
    public class Budget
    {   
        public ImmutableList<IMoney> Moneys { get; }
        
        public Month Month { get; }
        public uint Year { get; }
        public DateTimeOffset Created { get; }
        
        public Budget(Month month, uint year, ImmutableList<IMoney> moneys)
        {
            Month = month;
            Year = year;
            Created = DateTimeOffset.UtcNow;
            Moneys = moneys;
        }

        public Budget(Month month, uint year) : this(month, year, ImmutableList<IMoney>.Empty)
        {
        }
        
        public Amount Balance()
        {
            var currency = Moneys.First().Amount.Currency;
            
            Contracts.Require(Moneys.All(v => v.Amount.Currency == currency),
                "Sum is only possible for amount in the same currency");

            return Moneys.Aggregate(Amount.Create(0, currency),
                (current, value) => current + value.Amount);
        }
    }
}