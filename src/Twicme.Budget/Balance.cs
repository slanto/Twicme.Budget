using System.Collections.Generic;

namespace Twicme.Budget
{
    public sealed class Balance : ValueObject<Balance>
    {
        public Money FirstValue { get; }
        public Money SecondValue { get; }

        public Money Value => FirstValue - SecondValue;
        
        public Balance(Money firstValue, Money secondValue)
        {
            FirstValue = firstValue;
            SecondValue = secondValue;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstValue;
            yield return SecondValue;
        }
    }
}