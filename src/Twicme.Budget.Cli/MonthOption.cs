using System.Collections.Generic;
using Microsoft.Extensions.CommandLineUtils;

namespace Twicme.Budget.Cli
{
    public sealed class MonthOption : ValueObject<MonthOption>
    {
        public string Value => Command.Value();

        public bool Exists => Command.HasValue();
        
        public CommandOption Command { get; }
        
        private MonthOption(CommandLineApplication config)
        {
            Command = config.Option("-m |--month", "month [1-12]", CommandOptionType.SingleValue);
        }

        public static MonthOption Create(CommandLineApplication config) =>
            new MonthOption(config);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}