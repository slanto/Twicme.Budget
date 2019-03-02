using System.Collections.Generic;
using Microsoft.Extensions.CommandLineUtils;

namespace Twicme.Budget.Cli.Options
{
    public class RevenueOption : ValueObject<RevenueOption>
    {
        private readonly CommandOption _command;

        public bool Exists => _command.HasValue();
        
        public bool NotExists => !Exists;
        
        public static RevenueOption Create(CommandLineApplication config) =>
            new RevenueOption(config);
        
        public decimal Value => decimal.Parse(_command.Value());

        private RevenueOption(CommandLineApplication config)
        {
            _command = config.Option("-r |--revenue", "revenue [1300]", CommandOptionType.SingleValue);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}