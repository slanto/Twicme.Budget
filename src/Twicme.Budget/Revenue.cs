using System;
using System.Collections.Generic;

namespace Twicme.Budget
{
    public sealed class Revenue : ValueObject<Revenue>, IMoney
    {
        public Amount Amount { get; }
        public RevenueType Type { get; }
        public string Description { get; }
        public DateTimeOffset Created { get; }

        public Revenue(Amount amount, RevenueType type, string description = null)
        {
            Amount = amount;
            Type = type;
            Description = description;
            Created = DateTimeOffset.UtcNow;
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