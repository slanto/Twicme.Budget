using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Twicme.Budget
{
    public sealed class Expense : Money
    {
        public DateTimeOffset Created { get; }
        public ExpenseType Type { get; }
        public string Description { get; }

        public Expense(Money money, ExpenseType type, string description = null) : base(money.Amount, money.Currency)
        {
            Created = DateTimeOffset.UtcNow;
            Type = type;
            Description = description;
        }
    }
}