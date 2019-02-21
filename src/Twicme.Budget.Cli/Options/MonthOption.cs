using System.Collections.Generic;
using Microsoft.Extensions.CommandLineUtils;

namespace Twicme.Budget.Cli.Options
{
    public sealed class MonthOption : ValueObject<MonthOption>
    {
        public int Value => int.Parse(Command.Value());

        public bool Exists => Command.HasValue();
        
        public bool NotExists => !Exists;

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