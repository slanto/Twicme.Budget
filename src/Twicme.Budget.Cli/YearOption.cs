using System.Collections.Generic;
using Microsoft.Extensions.CommandLineUtils;

namespace Twicme.Budget.Cli
{
    public sealed class YearOption : ValueObject<YearOption>
    {
        public string Value => Command.Value();

        public bool Exists => Command.HasValue();
        
        public CommandOption Command { get; }
        
        private YearOption(CommandLineApplication config)
        {
            Command = config.Option("-y |--year", "year", CommandOptionType.SingleValue);
        }

        public static YearOption Create(CommandLineApplication config) =>
            new YearOption(config);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}