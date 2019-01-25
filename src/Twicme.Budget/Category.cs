using System.Collections.Generic;

namespace Twicme.Budget
{
    public class Category : ValueObject<Category>
    {
        public static readonly Category Food = new Category("Food");
        public static readonly Category Home = new Category("Home");
        public static readonly Category Car = new Category("Car");
        public static readonly Category PublicTransport = new Category("PublicTransport");
        public static readonly Category Media = new Category("Media");
        public static readonly Category HealthCare = new Category("HealthCare");
        public static readonly Category Cloths = new Category("Cloths");
        public static readonly Category Entertainment = new Category("Entertainment");
        public static readonly Category Education = new Category("Education");
        public static readonly Category Beauty = new Category("Beauty");
        public static readonly Category Thrift = new Category("Thrift");
        public static readonly Category Salary = new Category("Salary");
        public static readonly Category PartnerSalary = new Category("PartnerSalary");
        public static readonly Category Plus500 = new Category("500Plus");
        public static readonly Category Bonus = new Category("Bonus");
        public static readonly Category BankInterest = new Category("BankInterest");
        public static readonly Category Rental = new Category("Rental");
        
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