using System.Collections.Generic;

namespace Twicme.Budget
{
    public class Amount : ValueObject<Amount>
    { 
        public decimal Value { get; }
        public Currency Currency { get; }

        protected Amount(decimal value, Currency currency)
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
                "It is only possible to add money with the same currency");
            
            return new Amount(elem1.Value + elem2.Value, elem1.Currency);
        }
        
        public static Amount operator -(Amount minuend, Amount subtrahend)
        {
            Contracts.Require(minuend.Currency == subtrahend.Currency,
                "It is only possible to subtract money with the same currency");

            
            return new Amount(minuend.Value - subtrahend.Value, minuend.Currency);
        }
    }
}