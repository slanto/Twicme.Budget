using System;

namespace Twicme.Budget
{
    public class Expense
    {
        public DateTimeOffset Datetime { get; }
        public Money Money { get; }
        public string Description { get; }

        public Expense(DateTimeOffset datetime, Money money, string description)
        {
            Datetime = datetime;
            Money = money;
            Description = description;
        }
    }
}