namespace Twicme.Budget.Tests
{
    public class BudgetTestDataBuilder
    {
        public static Budget Build() =>
            new Budget(Month.April, 2019, Currency.PLN)
                .WithExpense(new Expense(Amount.Create(-50.55M, Currency.PLN), ExpenseType.Beauty))
                .WithExpense(new Expense(Amount.Create(-50.55M, Currency.PLN), ExpenseType.Car))
                .WithRevenue(new Revenue(Amount.Create(1250.55M, Currency.PLN), RevenueType.PartnerSalary))
                .WithRevenue(new Revenue(Amount.Create(1000, Currency.PLN), RevenueType.Salary));
        
    }
}