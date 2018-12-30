using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Twicme.Budget
{
    public sealed class Expense : Money
    {
        public DateTimeOffset Created { get; }
        public string Description { get; }

        public Expense(Money money, string description) : base(money.Amount, money.Currency)
        {
            Created = DateTimeOffset.UtcNow;
            Description = description;
        }
    }
}