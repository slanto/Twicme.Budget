namespace Twicme.Budget
{
    public static class CurrencyExtensions
    {
        public static Amount Zero(this Currency currency) => Amount.Create(0, currency);
    }
}