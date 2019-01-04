using System.Collections.Generic;

namespace Twicme.Budget
{
    public sealed class Balance : ValueObject<Balance>
    {
        public Amount FirstValue { get; }
        public Amount SecondValue { get; }

        public Amount Value => FirstValue - SecondValue;
        
        public Balance(Amount firstValue, Amount secondValue)
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