using System;
using System.Collections.Immutable;

namespace Twicme.Budget
{
    public static class MoneyExtensions
    {
        public static bool IsExpense(this IMoney money) => money as Expense != null;
        public static bool IsRevenue(this IMoney money) => money as Revenue != null;
        public static Expense AsExpense(this IMoney money) => money as Expense;
        public static Revenue AsRevenue(this IMoney money) => money as Revenue;
        
        
    }
}