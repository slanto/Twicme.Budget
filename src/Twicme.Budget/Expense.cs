using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Twicme.Budget
{
    public sealed class Expense : Amount
    {
        public DateTimeOffset Created { get; }
        public ExpenseType Type { get; }
        public string Description { get; }

        public Expense(Amount amount, ExpenseType type, string description = null) : base(amount.Value, amount.Currency)
        {
            Created = DateTimeOffset.UtcNow;
            Type = type;
            Description = description;
        }
    }
}