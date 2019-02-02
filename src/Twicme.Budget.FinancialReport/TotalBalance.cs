using System.Collections.Generic;

namespace Twicme.Budget.FinancialReport
{
    public class TotalBalance
    {
        private readonly Budget _plannedBudget;
        private readonly Budget _actualBudget;
     
        public TotalBalance(Budget budget)
        {
            _plannedBudget = budget;
        }

        public TotalBalance(Budget plannedBudget, Budget actualBudget)
        {
            _plannedBudget = plannedBudget;
            _actualBudget = actualBudget;
        }

        public Amount Amount =>
            _actualBudget == null
                ? _plannedBudget.Moneys.Sum(_plannedBudget.BaseCurrency)
                : _plannedBudget.Moneys.Sum(_plannedBudget.BaseCurrency) -
                  _actualBudget.Moneys.Sum(_actualBudget.BaseCurrency);

    }
}