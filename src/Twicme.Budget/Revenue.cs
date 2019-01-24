using System;
using System.Collections.Generic;

namespace Twicme.Budget
{
    public sealed class Revenue : Money<RevenueType>, IMoney
    {
        public override Amount Amount { get; }
        public override RevenueType Type { get; }
        public override string Description { get; }
        public override DateTimeOffset Created { get; }

        public Revenue(Amount amount, RevenueType type, DateTimeOffset created, string description = null)
        {
            Amount = amount;
            Type = type;
            Description = description;
            Created = created;
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