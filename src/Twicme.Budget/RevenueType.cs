namespace Twicme.Budget
{
    public class RevenueType
    {
        public static RevenueType Salary = new RevenueType("Salary");
        public static RevenueType PartnerSalary = new RevenueType("PartnerSalary");
        public static RevenueType Plus500 = new RevenueType("500Plus");
        public static RevenueType Bonus = new RevenueType("Bonus");
        public static RevenueType BankInterest = new RevenueType("BankInterest");
        public static RevenueType Rental = new RevenueType("Rental");
        
        public string Name { get; }

        private RevenueType(string name)
        {
            Name = name;
        }
    }
}