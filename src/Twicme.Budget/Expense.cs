using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Twicme.Budget
{
    public sealed class Expense : Money<ExpenseType>, IMoney
    {
        public override DateTimeOffset Created { get; }
        public override Amount Amount { get; }
        public override ExpenseType Type { get; }
        public override string Description { get; }

        public Expense(Amount amount, ExpenseType type, DateTimeOffset created, string description = null)
        {
            Contracts.Require(amount.Negative, "Expense can be only negative");

            Created = created;
            Amount = amount;
            Type = type;
            Description = description;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Created;
            yield return Amount;
            yield return Type;
            yield return Description;
        }
    }
}