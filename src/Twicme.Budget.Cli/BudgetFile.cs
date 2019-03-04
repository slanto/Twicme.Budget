using System;
using System.IO;
using Twicme.Budget.Store;

namespace Twicme.Budget.Cli
{
    public class BudgetFile
    {
        private readonly Month _month;
        private readonly Currency _currency;
        
        private Money _money = Money.Zero;
        
        private FileName FileName => _isPlanningMode
            ? new FileName($"budget-{_month.Year}-{_month.MonthName.Index}-{_currency.Symbol}-plan")
            : new FileName($"budget-{_month.Year}-{_month.MonthName.Index}-{_currency.Symbol}");
        
        private bool _isPlanningMode;
        
        public BudgetFile(Month month, Currency currency)
        {
            _month = month;
            _currency = currency;
        }

        public BudgetFile WithMoney(Money money)
        {
            _money = money;
            return this;
        }

        public BudgetFile InPlanningMode()
        {
            _isPlanningMode = true;
            return this;
        }
        
        public void Store()
        {
            if (!FileName.Exists)
            {
                throw new InvalidOperationException($"There is no budget file {FileName.Path}.");
            }

            var budget = new JsonContent(File.ReadAllText(FileName.Path))
                .ToBudget();

            var budgetToStore =
                new JsonBudget(_money.IsExpense() ? 
                    budget.WithExpense(_money) : 
                    budget.WithRevenue(_money));
           
            File.WriteAllText(FileName.Path,budgetToStore.Content.Value);
        }
    }
}