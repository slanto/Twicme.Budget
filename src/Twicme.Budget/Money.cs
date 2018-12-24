namespace Twicme.Budget
{
    public class Money
    {
        public decimal Value { get; }
        public string Currency { get; }

        public Money(decimal value, string currency)
        {
            Value = value;
            Currency = currency;
        }
    }
}