namespace Twicme.Budget
{
    public class Money
    {
        public decimal Value { get; }
        public string Currency { get; }

        private Money(decimal value, string currency)
        {
            Value = value;
            Currency = currency;
        }

        public static Money CreateZloty(decimal value)
        {
            return new Money(value, "PLN");
        }
    }
}