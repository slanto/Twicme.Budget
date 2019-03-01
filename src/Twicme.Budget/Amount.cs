using System.Collections.Generic;

namespace Twicme.Budget
{
    public class Amount : ValueObject<Amount>
    { 
        public static readonly Amount Zero = new Amount(0, Currency.PLN);
        
        public decimal Value { get; }
        public Currency Currency { get; }

        public bool Negative => Value < 0;
        public bool Positive => !Negative;

        private Amount(decimal value, Currency currency)
        {
            Value = decimal.Round(value, 2);
            Currency = currency;
        }

        public static Amount Create(decimal amount, Currency currency)
        {   
            return new Amount(amount, currency);
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Currency;
        }

        public static Amount operator +(Amount elem1, Amount elem2)
        {
            Contracts.Require(elem1.Currency == elem2.Currency,
                "It is only possible to add amount in the same currency");
            
            return new Amount(elem1.Value + elem2.Value, elem1.Currency);
        }
        
        public static Amount operator -(Amount minuend, Amount subtrahend)
        {
            Contracts.Require(minuend.Currency == subtrahend.Currency,
                "It is only possible to subtract amount in the same currency");

            return new Amount(minuend.Value - subtrahend.Value, minuend.Currency);
        }
    }
}