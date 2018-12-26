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
            if (amount <= 0)
            {
                throw new ArgumentException($"{amount} cannot be less or equals to zero");
            }
            
            return new Money(amount, "PLN");
        }
    }
}