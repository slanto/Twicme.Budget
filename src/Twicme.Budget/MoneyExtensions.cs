using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Twicme.Budget
{
    public static class MoneyExtensions
    {
        public static bool IsExpense(this Money money) => money.Amount.Negative;
        public static bool IsRevenue(this Money money) => money.Amount.Positive;
        public static Amount Sum(this IEnumerable<Money> moneys, Currency currency) => 
            moneys.Aggregate(currency.Zero(), (amount, revenue) => amount + revenue.Amount);
    }
}