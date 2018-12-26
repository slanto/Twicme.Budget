using System;
using System.Runtime.CompilerServices;

namespace Twicme.Budget
{
    public class Expense
    {
        public DateTimeOffset Created { get; }
        public Money Money { get; }
        public string Description { get; }

        public Expense(Money money, IClock clock, string description)
        {
            Created = clock.UtcNow;
            Money = money;
            Description = description;
        }

        public Expense(Money money, IClock clock) : this(money, clock, string.Empty)
        {
        }
    }
}