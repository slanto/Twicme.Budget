namespace Twicme.Budget
{
    public class RevenueType
    {
        public static readonly RevenueType Salary = new RevenueType("Salary");
        public static readonly RevenueType PartnerSalary = new RevenueType("PartnerSalary");
        public static readonly RevenueType Plus500 = new RevenueType("500Plus");
        public static readonly RevenueType Bonus = new RevenueType("Bonus");
        public static readonly RevenueType BankInterest = new RevenueType("BankInterest");
        public static readonly RevenueType Rental = new RevenueType("Rental");
        
        public string Name { get; }

        private RevenueType(string name)
        {
            Name = name;
        }

        public static RevenueType Create(string name) => new RevenueType(name);
    }
}