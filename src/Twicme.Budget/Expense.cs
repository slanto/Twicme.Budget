using System;
using System.Runtime.CompilerServices;

namespace Twicme.Budget
{
    public class Expense
    {
        public DateTimeOffset Created { get; }
        public Money Money { get; }
        public string Description { get; }

        public Expense(Money money, string description)
        {
            Created = DateTimeOffset.UtcNow;
            Money = money;
            Description = description;
        }
    }
}