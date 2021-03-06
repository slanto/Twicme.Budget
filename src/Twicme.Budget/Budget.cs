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
        public DateTimeOffset CreatedOn { get; }

        public Budget(Month month, Currency baseCurrency, DateTimeOffset createdOn, ImmutableList<Money> moneys = null)
        {
            Month = month;
            BaseCurrency = baseCurrency;
            CreatedOn = createdOn;
            Moneys = moneys ?? ImmutableList.Create<Money>();
        }

        public Budget(Month month, Currency baseCurrency)
            : this(month, baseCurrency, DateTimeOffset.UtcNow, ImmutableList<Money>.Empty)
        {
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Month;
            yield return CreatedOn;
            yield return Moneys;
        }

        public override string ToString()
        {
            return $"month: {Month.MonthName.Name}, year: {Month.Year}, currency: {BaseCurrency}";
        }
    }
}