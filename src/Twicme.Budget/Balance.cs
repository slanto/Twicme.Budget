using System.Collections.Generic;
using System.Linq;

namespace Twicme.Budget
{
    public class Balance
    {
        public IReadOnlyList<Revenue> Revenues { get; }
        public IReadOnlyList<Expense> Expenses { get; }
        
        public Money TotalRevenue => Money.CreateZloty(Revenues.Sum(r => r.Money.Amount));
        public Money TotalExpense => Money.CreateZloty(Expenses.Sum(r => r.Money.Amount));

        public Balance() : this(new List<Revenue>(), new List<Expense>())
        {
        }
        
        public Balance(IReadOnlyList<Revenue> revenues, IReadOnlyList<Expense> expenses)
        {
            Revenues = revenues;
            Expenses = expenses;
        }

        public Balance(params Revenue[] revenues)
        {
            Revenues = revenues;
            Expenses = new List<Expense>();
        }
        
        public Balance(params Expense[] expenses)
        {
            Expenses = expenses;
            Revenues = new List<Revenue>();
        }
    }
}