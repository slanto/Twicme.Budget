using System.Collections.Generic;
using Microsoft.Extensions.CommandLineUtils;

namespace Twicme.Budget.Cli.Options
{
    public class AmountOption : ValueObject<AmountOption>
    {
        public string Value => _command.Value();

        public bool Exists => _command.HasValue();
        
        public bool NotExists => !Exists;

        private readonly CommandOption _command;
        
        private AmountOption(CommandLineApplication config)
        {
            _command = config.Option("-a |--amount", "amount [12.56]", CommandOptionType.SingleValue);
        }

        public static AmountOption Create(CommandLineApplication config) =>
            new AmountOption(config);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}