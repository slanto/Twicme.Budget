using Microsoft.Extensions.CommandLineUtils;

namespace Twicme.Budget.Cli.CommandBuilders
{
    public class BudgetCommandBuilder
    {
        private readonly CommandLineApplication _application;

        public BudgetCommandBuilder(CommandLineApplication application)
        {
            _application = application;
        }
        
        public CommandLineApplication Build()
        {
            return _application.Command("budget", config =>
            {
                config.Name = "budget";
                config.Description = "budget";
                config.HelpOption(HelpFlagTemplate.Value);          
               
                config.OnExecute(() =>
                {
                    config.ShowHelp();
                    return ErrorCode.Value;
                });
            }, false);    
        }
    }
}