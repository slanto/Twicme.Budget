using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Twicme.Budget
{
    public class Expense : ValueObject<Expense>, IMoney
    {
        public DateTimeOffset Created { get; }
        public Amount Amount { get; }
        public ExpenseType Type { get; }
        public string Description { get; }

        public Expense(Amount amount, ExpenseType type, string description = null)
        {
            Contracts.Require(amount.Negative, "Expense can be only negative");
            
            Created = DateTimeOffset.UtcNow;
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