using System.Collections.Generic;
using System.Linq;

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

        public static Currency Create(string symbol)
        {
            Contracts.Require(All.Select(s => s.Symbol).Contains(symbol), $"Currency not found {symbol}");
            
            return new Currency(symbol);
        }

        public static IEnumerable<Currency> All => new[] {PLN, USD};
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Symbol;
        }

        public override string ToString() => Symbol;
    }
}