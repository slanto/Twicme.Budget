using System;
using System.Collections.Generic;

namespace Twicme.Budget
{
    public class Money : ValueObject<Money>
    {
        public static Money Zero => new Money(Amount.Zero, Category.NotDefined, Description.Empty);
        
        public DateTimeOffset Created { get; }
        public Amount Amount { get; }
        public Category Category { get; }
        public Description Description { get; }
        
        public Money(Amount amount, Category category, DateTimeOffset created, Description description = null)
        {
            Amount = amount;
            Category = category;
            Description = description ?? Description.Empty;
            Created = created;
        }
        
        public Money(Amount amount, Category category, Description description = null) : 
            this(amount, category, DateTimeOffset.UtcNow, description)
        {
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