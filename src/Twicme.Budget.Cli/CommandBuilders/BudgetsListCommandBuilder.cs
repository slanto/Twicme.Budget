using Microsoft.Extensions.CommandLineUtils;

namespace Twicme.Budget.Cli.CommandBuilders
{
    public class BudgetsListCommandBuilder
    {
        private readonly CommandLineApplication _application;
        
        public BudgetsListCommandBuilder(CommandLineApplication application)
        {
            _application = application;
        }
        
        public CommandLineApplication Build()
        {
            return _application.Command("list", config =>
            {
                config.Name = "list";
                config.Description = "display list of existing budgets";
                config.HelpOption(HelpFlagTemplate.Value);

                
                config.OnExecute(() => 1);
            }, false);    
        }
    }
}