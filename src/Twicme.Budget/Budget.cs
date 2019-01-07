using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Twicme.Budget
{
    public class Budget : ValueObject<Budget>
    {   
        public ImmutableList<IMoney> Moneys { get; }
        public Month Month { get; }
        public uint Year { get; }
        public Currency BaseCurrency { get; }
        public DateTimeOffset Created { get; }
        
        public Budget(Month month, uint year, Currency baseCurrency, ImmutableList<IMoney> moneys)
        {
            Month = month;
            Year = year;
            BaseCurrency = baseCurrency;
            Created = DateTimeOffset.UtcNow;
            Moneys = moneys;
        }

        public Budget(Month month, uint year, Currency baseCurrency) : this(month, year, baseCurrency,
            ImmutableList<IMoney>.Empty)
        {
        }

        public Amount Balance()
        {          
            Contracts.Require(Moneys.All(v => v.Amount.Currency == BaseCurrency),
                "Sum is only possible for amount in the same currency");

            return Moneys.Aggregate(BaseCurrency.Zero(),
                (current, value) => current + value.Amount);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Month;
            yield return Year;
            yield return Created;
            yield return Moneys;
        }
    }
}