using System.Collections.Generic;
using System.Linq;

namespace Twicme.Budget
{
    public class Balance
    {
        public IReadOnlyList<Revenue> Revenues { get; }
        public IReadOnlyList<Expense> Expenses { get; }
        
        public Money TotalRevenue => Money.CreateZloty(Revenues.Sum(r => r.Money.Amount));

        public Balance()
        {
            Revenues = new List<Revenue>();
            Expenses = new List<Expense>();
        }

        public Balance(params Revenue[] revenues)
        {
            Revenues = revenues;
        }
    }
}