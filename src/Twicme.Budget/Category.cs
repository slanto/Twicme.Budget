using System;
using System.Collections.Generic;
using System.Linq;

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
            Name = name.ToLowerInvariant();
        }
        
        public static Category Create(string name) => new Category(name);
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }

        public static Category From(string name)
        {
            if (All.Contains(Create(name)))
            {
                return All.Single(c=>c.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            }
            
            throw new InvalidOperationException($"Category {name} does not exists.");
        }

        public static IEnumerable<Category> All => new[]
        {
            HealthAndBeauty, Education, CarAndTransport, HomeAndBills, BasicExpenditure, EntertainmentAndTravelling,
            ClothesAndShoes, Salary, OtherIncome
        };
    }
}