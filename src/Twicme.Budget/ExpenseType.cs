namespace Twicme.Budget
{
    public class ExpenseType
    {
        public static readonly ExpenseType Food = new ExpenseType("Food");
        public static readonly ExpenseType Home = new ExpenseType("Home");
        public static readonly ExpenseType Car = new ExpenseType("Car");
        public static readonly ExpenseType PublicTransport = new ExpenseType("PublicTransport");
        public static readonly ExpenseType Media = new ExpenseType("Media");
        public static readonly ExpenseType HealthCare = new ExpenseType("HealthCare");
        public static readonly ExpenseType Cloths = new ExpenseType("Cloths");
        public static readonly ExpenseType Entertainment = new ExpenseType("Entertainment");
        public static readonly ExpenseType Education = new ExpenseType("Education");
        public static readonly ExpenseType Beauty = new ExpenseType("Beauty");
        public static readonly ExpenseType Thrift = new ExpenseType("Thrift");
        
        public string Name { get; }

        private ExpenseType(string name)
        {
            Name = name;
        }
    }
}