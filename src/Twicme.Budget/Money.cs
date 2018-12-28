using System;

namespace Twicme.Budget
{
    public class Money
    {
        public decimal Amount { get; }
        public string Currency { get; }

        private Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public static Money CreateZloty(decimal amount)
        {   
            return new Money(amount, "PLN");
        }
    }
}