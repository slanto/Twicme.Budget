using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Twicme.Budget
{
    public sealed class Expense : IMoney
    {
        public DateTimeOffset Created { get; }
        public Amount Amount { get; }
        public ExpenseType Type { get; }
        public string Description { get; }

        public Expense(Amount amount, ExpenseType type, string description = null)
        {
            Created = DateTimeOffset.UtcNow;
            Amount = amount;
            Type = type;
            Description = description;
        }
    }
}