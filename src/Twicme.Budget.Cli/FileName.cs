using System.Collections.Generic;
using System.IO;

namespace Twicme.Budget.Cli
{
    public class FileName : ValueObject<FileName>
    {
        public static FileName Real(Budget budget) => 
            new FileName($"budget-{budget.Month.Year}-{budget.Month.MonthName.Index}");
        
        public static FileName Planned(Budget budget) => 
            new FileName($"budget-{budget.Month.Year}-{budget.Month.MonthName.Index}-plan");

        public bool Exists => File.Exists(Path);
        
        public string Name { get; }

        public string Path => $"{Name}.json";
        private FileName(string name)
        {
            Name = name;
        }
        
        public static FileName Create(string name) => new FileName(name);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}