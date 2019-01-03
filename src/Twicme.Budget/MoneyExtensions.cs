using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Twicme.Budget
{
    public static class MoneyExtensions
    {
        private static Money SumMoney(IReadOnlyList<Money> money)
        {
            var currency = money.First().Currency;
            
            Contracts.Require(money.All(v => v.Currency == currency),
                "Sum is only possible for money in the same currency");

            return money.Aggregate(Money.Create(0, currency),
                (current, value) => current + value);
        }

        public static Money Sum(this ImmutableList<Expense> expense) => SumMoney(expense);
        public static Money Sum(this ImmutableList<Revenue> revenue) => SumMoney(revenue);       
    }
}