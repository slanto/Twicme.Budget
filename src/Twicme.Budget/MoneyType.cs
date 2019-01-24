namespace Twicme.Budget
{
    public abstract class MoneyType : ValueObject<MoneyType>
    {
        public abstract string Name { get; }
    }
}