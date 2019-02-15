using Microsoft.Extensions.CommandLineUtils;

namespace Twicme.Budget.Cli
{
    public class AddCommandBuilder
    {
        private readonly CommandLineApplication _application;
        
        private const string HelpFlagTemplate = "-? |-h |--help";
        
        public AddCommandBuilder(CommandLineApplication application)
        {
            _application = application;
        }
        
        public CommandLineApplication Build()
        {
            return _application.Command("add", config =>
            {
                config.Name = "add";
                config.Description = "add revenue or expense to budget";
                config.HelpOption(HelpFlagTemplate);

                
                config.OnExecute(() => 1);
            }, false);    
        }
    }
}