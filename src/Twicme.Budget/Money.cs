using System;
using System.Collections.Generic;

namespace Twicme.Budget
{
    public class Money : ValueObject<Money>
    {
        public DateTimeOffset Created { get; }
        public Amount Amount { get; }
        public Category Category { get; }
        public string Description { get; }
        
        public Money(Amount amount, Category category, DateTimeOffset created, string description = null)
        {
            Amount = amount;
            Category = category;
            Description = description;
            Created = created;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Created;
            yield return Amount;
            yield return Category;
            yield return Description;
        }
    }
}