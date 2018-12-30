using System;
using System.Collections.Generic;

namespace Twicme.Budget
{
    public class Money : ValueObject<Money>
    {
        public const string PLNCurrency = "PLN";
        public const string USDCurrency = "USD";
        
        public decimal Amount { get; }
        public string Currency { get; }

        private Money(decimal amount, string currency)
        {
            Amount = decimal.Round(amount, 2);
            Currency = currency;
        }

        public static Money CreateZloty(decimal amount)
        {   
            return new Money(amount, PLNCurrency);
        }

        public static Money CreateDollar(decimal amount)
        {   
            return new Money(amount, USDCurrency);
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }

        public static Money operator +(Money elem1, Money elem2)
        {
            if (elem1.Currency != elem2.Currency)
            {
                throw new Exception("Cannot add money with different currency");
            }
            
            return new Money(elem1.Amount + elem2.Amount, elem1.Currency);
        }
        
        public static Money operator -(Money minuend, Money subtrahend)
        {
            if (minuend.Currency != subtrahend.Currency)
            {
                throw new Exception("Cannot subtract money with different currency");
            }
            
            return new Money(minuend.Amount + subtrahend.Amount, minuend.Currency);
        }
    }
}