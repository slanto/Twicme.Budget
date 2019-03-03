using System.Collections.Generic;
using Microsoft.Extensions.CommandLineUtils;

namespace Twicme.Budget.Cli.Options
{
    public class PlanningModeOption : ValueObject<MonthOption>
    {
        public string Value => _command.Value();

        public bool Exists => _command.HasValue();
        
        public bool NotExists => !Exists;

        private readonly CommandOption _command;
        
        private PlanningModeOption(CommandLineApplication config)
        {
            _command = config.Option("--planning-mode", "budget planning mode", CommandOptionType.NoValue);
        }

        public static PlanningModeOption Create(CommandLineApplication config) =>
            new PlanningModeOption(config);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}