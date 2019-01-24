using System;

namespace Twicme.Budget
{
    public abstract class Money<TType> : ValueObject<Money<TType>> where TType: MoneyType 
    {
        public abstract DateTimeOffset Created { get; }
        public abstract Amount Amount { get; }
        public abstract TType Type { get; }
        public abstract string Description { get; }
    }
}