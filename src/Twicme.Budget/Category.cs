using System.Collections.Generic;

namespace Twicme.Budget
{
    public class Category : ValueObject<Category>
    {
        public static readonly Category HealthAndBeauty = new Category("Health and beauty");
        public static readonly Category Education = new Category("Education");
        public static readonly Category CarAndTransport = new Category("Car and transport");
        public static readonly Category HomeAndBills = new Category("Home and bills");
        public static readonly Category BasicExpenditure = new Category("Basic expenditure");
        public static readonly Category EntertainmentAndTravelling = new Category("Entertainment and travelling");
        public static readonly Category ClothesAndShoes = new Category("Clothes and shoes");
        public static readonly Category Salary = new Category("Salary");
        public static readonly Category OtherIncome = new Category("Other income");
        
        public string Name { get; }

        private Category(string name)
        {
            Name = name;
        }
        
        public static Category Create(string name) => new Category(name);
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}