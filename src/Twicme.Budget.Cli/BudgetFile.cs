using System;
using System.IO;
using Twicme.Budget.Store;

namespace Twicme.Budget.Cli
{
    public class BudgetFile
    {
        private readonly Month _month;
        
        private Money _money = Money.Zero;

        private FileName FileName => _isPlanningMode
            ? new FileName($"budget-{_month.Year}-{_month.MonthName.Index}-plan")
            : new FileName($"budget-{_month.Year}-{_month.MonthName.Index}");
        
        private bool _isPlanningMode;
        
        public BudgetFile(Month month)
        {
            _month = month;
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

            if (_money == Money.Zero)
            {
                throw new InvalidOperationException("Invalid money's amount");
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