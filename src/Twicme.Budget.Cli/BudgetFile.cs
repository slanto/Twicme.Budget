using System.IO;
using Twicme.Budget.Store;

namespace Twicme.Budget.Cli
{
    public class BudgetFile
    {
        private readonly Budget _budget;
        private readonly FileName _fileName;

        public BudgetFile(Budget budget, FileName fileName)
        {
            _budget = budget;
            _fileName = fileName;
        }

        public void Save() =>
            File.WriteAllText(_fileName.Path,
                new JsonBudget(_budget).Content.Value);
        
        public bool NotExists =>
            !FileName.Real(_budget).Exists || !FileName.Planned(_budget).Exists;
    }
}