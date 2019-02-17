using System.Collections.Generic;
using Microsoft.Extensions.CommandLineUtils;

namespace Twicme.Budget.Cli
{
    public sealed class CurrencyOption : ValueObject<CurrencyOption>
    {
        public string Value => Command.Value();

        public bool Exists => Command.HasValue();
        
        public bool NotExists => !Exists;

        public CommandOption Command { get; }
        
        private CurrencyOption(CommandLineApplication config)
        {
            Command = config.Option("-c |--currency", "currency [PLN, USD]", CommandOptionType.SingleValue);
        }

        public static CurrencyOption Create(CommandLineApplication config) =>
            new CurrencyOption(config);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}