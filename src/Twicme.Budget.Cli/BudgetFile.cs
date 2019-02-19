using System.IO;
using Twicme.Budget.Store;

namespace Twicme.Budget.Cli
{
    public class BudgetFile
    {
        public Budget Budget { get; }
        public FileName FileName { get; }

        public BudgetFile(Budget budget, FileName fileName)
        {
            Budget = budget;
            FileName = fileName;
        }

        public void Save() =>
            File.WriteAllText(FileName.Path,
                new JsonBudget(Budget).Content.Value);
        
        public bool NotExists =>
            !FileName.Real(Budget).Exists || !FileName.Planned(Budget).Exists;

        public BudgetFile Load() =>
            new BudgetFile(new JsonContent(File.ReadAllText(FileName.Path)).ToBudget(), FileName);
    }
}