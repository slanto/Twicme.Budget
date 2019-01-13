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

        public Budget(Month month, uint year, Currency baseCurrency, ImmutableList<IMoney> moneys) : this(month, year,
            baseCurrency, moneys, new Clock())
        {
        }

        public Budget(Month month, uint year, Currency baseCurrency) : this(month, year, baseCurrency,
            ImmutableList<IMoney>.Empty, new Clock())
        {
        }

        public Budget(Month month, uint year, Currency baseCurrency, ImmutableList<IMoney> moneys, IClock clock)
        {
            Month = month;
            Year = year;
            BaseCurrency = baseCurrency;
            Created = clock.NowUtc;
            Moneys = moneys;
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