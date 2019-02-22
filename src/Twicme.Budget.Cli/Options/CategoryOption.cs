using System.Collections.Generic;
using Microsoft.Extensions.CommandLineUtils;

namespace Twicme.Budget.Cli.Options
{
    public class CategoryOption : ValueObject<CategoryOption>
    {
        public string Value => _command.Value();

        public bool Exists => _command.HasValue();
        
        public bool NotExists => !Exists;

        private readonly CommandOption _command;
        
        private CategoryOption(CommandLineApplication config)
        {
            _command = config.Option("-t |--category", "category [salary, education]", CommandOptionType.SingleValue);
        }

        public static CategoryOption Create(CommandLineApplication config) =>
            new CategoryOption(config);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}