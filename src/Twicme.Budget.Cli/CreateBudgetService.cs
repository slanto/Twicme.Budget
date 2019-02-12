using System.IO;
using Twicme.Budget.Store;

namespace Twicme.Budget.Cli
{
    public class CreateBudgetService
    {
        private readonly ILog _log;

        public CreateBudgetService(ILog log)
        {
            _log = log;
        }

        public int Execute(int year, int monthNumber, string currency)
        {
            var month = Month.Create(year, MonthName.Create(monthNumber));
            var baseCurrency = Currency.Create(currency);
            
            var budget = new Budget(month, baseCurrency);

            if (BudgetNotExists(budget))
            {
                Save(budget, FileName.Real(budget));
                Save(budget, FileName.Planned(budget));

                _log.Write($"Budget {budget} has been created.");
                return 0;
            }

            _log.Write($"Budget {budget} already exists.");
            return 1;
        }
        
        private static bool BudgetNotExists(Budget budget) =>
            !FileName.Real(budget).Exists || !FileName.Planned(budget).Exists;

        private static void Save(Budget budget, FileName fileName) =>
            File.WriteAllText(fileName.Path, 
                new JsonBudget(budget).Content.Value);
    }
}