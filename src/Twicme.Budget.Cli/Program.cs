using System;
using Microsoft.Extensions.CommandLineUtils;
using Twicme.Budget.Cli.CommandBuilders;

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
            var app = new CommandLineApplication();
            app.HelpOption(HelpFlagTemplate);

            var budget = new BudgetCommandBuilder(app).Build();
            
            new CreateBudgetCommandBuilder(budget).Build();
//            new BudgetsListCommandBuilder(budget).Build();
//            new AddCommandBuilder(budget).Build();

            return app.Execute(args);
        }
    }
}