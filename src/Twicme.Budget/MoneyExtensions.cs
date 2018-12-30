using System.Collections.Generic;
using System.Linq;

namespace Twicme.Budget
{
    public static class MoneyExtensions
    {
        public static Money Balance(this Money value, Money money)
        {
            return value - money;
        }

        public static Money Sum(this IEnumerable<Money> values)
        {
            var money = values.ToList();
            var currency = money.First().Currency;
            
            Contracts.Require(money.All(v => v.Currency == currency),
                "Sum is only possible for money in the same currency");

            return money.Aggregate(Money.Create(0, currency),
                (current, value) => current + value);
        }
    }
}