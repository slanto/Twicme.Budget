using System.Collections.Generic;
using Microsoft.Extensions.CommandLineUtils;

namespace Twicme.Budget.Cli.Options
{
    public sealed class CurrencyOption : ValueObject<CurrencyOption>
    {
        public string Value => _command.Value();

        public bool Exists => _command.HasValue();
        
        public bool NotExists => !Exists;

        private readonly CommandOption _command;
        
        private CurrencyOption(CommandLineApplication config)
        {
            _command = config.Option("-c |--currency", "currency [PLN, USD]", CommandOptionType.SingleValue);
        }

        public static CurrencyOption Create(CommandLineApplication config) =>
            new CurrencyOption(config);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}