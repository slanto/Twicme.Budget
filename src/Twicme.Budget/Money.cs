using System.Collections.Generic;

namespace Twicme.Budget
{
    public class Money : ValueObject<Money>
    { 
        public decimal Amount { get; }
        public Currency Currency { get; }

        protected Money(decimal amount, Currency currency)
        {
            Amount = decimal.Round(amount, 2);
            Currency = currency;
        }

        public static Money Create(decimal amount, Currency currency)
        {   
            return new Money(amount, currency);
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }

        public static Money operator +(Money elem1, Money elem2)
        {
            Contracts.Require(elem1.Currency == elem2.Currency,
                "It is only possible to add money with the same currency");
            
            return new Money(elem1.Amount + elem2.Amount, elem1.Currency);
        }
        
        public static Money operator -(Money minuend, Money subtrahend)
        {
            Contracts.Require(minuend.Currency == subtrahend.Currency,
                "It is only possible to subtract money with the same currency");

            
            return new Money(minuend.Amount - subtrahend.Amount, minuend.Currency);
        }
    }
}