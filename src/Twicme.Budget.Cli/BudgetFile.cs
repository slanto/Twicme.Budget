using System.IO;
using Twicme.Budget.Store;

namespace Twicme.Budget.Cli
{
    public class BudgetFile
    {
        public Budget Budget { get; }
        public FileName FileName { get; }

        public bool Exists => FileName.Exists;

        public bool NotExists => !Exists;
        
        public BudgetFile(Budget budget, FileName fileName)
        {
            Budget = budget;
            FileName = fileName;
        }

        public void Save() =>
            File.WriteAllText(FileName.Path,
                new JsonBudget(Budget).Content.Value);
        
        public BudgetFile Load() =>
            new BudgetFile(new JsonContent(File.ReadAllText(FileName.Path)).ToBudget(), FileName);
    }
}