using System.Collections.Generic;
using Microsoft.Extensions.CommandLineUtils;

namespace Twicme.Budget.Cli.Options
{
    public class ExpenseOption : ValueObject<ExpenseOption>
    {
        private readonly CommandOption _command;

        public bool Exists => _command.HasValue();
        
        public bool NotExists => !Exists;
        
        public decimal Value => decimal.Parse(_command.Value());

        private ExpenseOption(CommandLineApplication config)
        {
            _command = config.Option("-e |--expense", "expense [-1499.99]", CommandOptionType.SingleValue);
        }

        public static ExpenseOption Create(CommandLineApplication config) =>
            new ExpenseOption(config);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}