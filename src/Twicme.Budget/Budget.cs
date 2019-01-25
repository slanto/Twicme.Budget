using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Twicme.Budget
{
    public class Budget : ValueObject<Budget>
    {   
        public ImmutableList<IMoney> Moneys { get; }
        public MonthName MonthName { get; }
        public uint Year { get; }
        public Currency BaseCurrency { get; }
        public DateTimeOffset Created { get; }

        public Budget(MonthName monthName, uint year, Currency baseCurrency, DateTimeOffset created, ImmutableList<IMoney> moneys)
        {
            MonthName = monthName;
            Year = year;
            BaseCurrency = baseCurrency;
            Created = created;
            Moneys = moneys;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return MonthName;
            yield return Year;
            yield return Created;
            yield return Moneys;
        }
    }
}