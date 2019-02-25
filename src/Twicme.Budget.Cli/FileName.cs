using System.Collections.Generic;
using System.IO;

namespace Twicme.Budget.Cli
{
    public class FileName : ValueObject<FileName>
    {
        public bool Exists => File.Exists(Path);
        
        public string Name { get; }

        public string Path => $"{Name}.json";
        
        public FileName(string name)
        {
            Name = name;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}