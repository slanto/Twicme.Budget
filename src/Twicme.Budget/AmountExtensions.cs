using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Twicme.Budget
{
    public static class AmountExtensions
    {
        private static Amount SumMoney(IReadOnlyList<Amount> amount)
        {
            var currency = amount.First().Currency;
            
            Contracts.Require(amount.All(v => v.Currency == currency),
                "Sum is only possible for amount in the same currency");

            return amount.Aggregate(Amount.Create(0, currency),
                (current, value) => current + value);
        }

        public static Amount Sum(this ImmutableList<Expense> expense) => SumMoney(expense);
        public static Amount Sum(this ImmutableList<Revenue> revenue) => SumMoney(revenue);       
    }
}