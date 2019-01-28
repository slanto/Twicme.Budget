using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Twicme.Budget
{
    public class Budget : ValueObject<Budget>
    {   
        public ImmutableList<Money> Moneys { get; }
        public Month Month { get; }
        public Currency BaseCurrency { get; }
        public DateTimeOffset Created { get; }

        public Budget(Month month, Currency baseCurrency, DateTimeOffset created, ImmutableList<Money> moneys)
        {
            Month = month;
            BaseCurrency = baseCurrency;
            Created = created;
            Moneys = moneys;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Month;
            yield return Created;
            yield return Moneys;
        }
    }
}