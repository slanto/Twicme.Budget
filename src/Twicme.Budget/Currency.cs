using System.Collections.Generic;

namespace Twicme.Budget
{
    public class Currency : ValueObject<Currency>
    {
        public static readonly Currency PLN = new Currency("PLN");
        public static readonly Currency USD = new Currency("USD");
        
        public string Symbol { get; }

        private Currency(string symbol)
        {
            Symbol = symbol;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Symbol;
        }
    }
}