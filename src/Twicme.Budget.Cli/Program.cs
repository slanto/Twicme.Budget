using System;
using Microsoft.Extensions.CommandLineUtils;

namespace Twicme.Budget.Cli
{
    class Program
    {
        const string HelpFlagTemplate = "-? |-h |--help";
           
        static void Main(string[] args)
        {
            try
            {
                var result = Run(args);
                Environment.Exit(result);
            }
            catch (Exception e)
            {
                var message = e.GetBaseException().Message;
                Console.Error.WriteLine(message);
            }
        }

        private static int Run(string[] args)
        {
            var application = new CommandLineApplication();
            application.HelpOption(HelpFlagTemplate);

            new CreateBudgetCommandBuilder(application)
                .Build();
            
            new BudgetsListCommandBuilder(application)
                .Build();

            new AddCommandBuilder(application).Build();

            return application.Execute(args);
        }
    }
}