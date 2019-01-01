using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Twicme.Budget
{
    public class MoneyCollection<T> : IEnumerable<T> where T: Money
    {
        public static MoneyCollection<T> Empty => new MoneyCollection<T>();
        
        private readonly IEnumerable<T> _money;

        public MoneyCollection(IEnumerable<T> money)
        {
            _money = money;
        }
        
        public MoneyCollection(params T[] money) : this(money.ToList())
        {
        }
        
        public Money Sum()
        {
            var money = _money.ToList();
            var currency = money.First().Currency;
            
            Contracts.Require(money.All(v => v.Currency == currency),
                "Sum is only possible for money in the same currency");

            return money.Aggregate(Money.Create(0, currency),
                (current, value) => current + value);
        }
        
        public IEnumerator<T> GetEnumerator() => _money.GetEnumerator();
        
        IEnumerator IEnumerable.GetEnumerator() => _money.GetEnumerator();
    }
}