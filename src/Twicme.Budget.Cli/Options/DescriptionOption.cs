using System.Collections.Generic;
using Microsoft.Extensions.CommandLineUtils;

namespace Twicme.Budget.Cli.Options
{
    public sealed class DescriptionOption : ValueObject<DescriptionOption>
    {
        public string Value => _command.Value();

        public bool Exists => _command.HasValue();

        public bool NotExists => !Exists;

        private readonly CommandOption _command;

        private DescriptionOption(CommandLineApplication config)
        {
            _command = config.Option("-d |--description", "description", CommandOptionType.SingleValue);
        }

        public static DescriptionOption Create(CommandLineApplication config) =>
            new DescriptionOption(config);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}