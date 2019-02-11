using System;
using System.IO;
using System.Net;
using Microsoft.Extensions.CommandLineUtils;
using Twicme.Budget.Store;

namespace Twicme.Budget.Cli
{
    public class CreateBudgetCommandBuilder
    {
        private readonly CommandLineApplication _application;
        
        private const string HelpFlagTemplate = "-? |-h |--help";
        
        public CreateBudgetCommandBuilder(CommandLineApplication application)
        {
            _application = application;
        }
        
        public CommandLineApplication Build()
        {
            return _application.Command("create", config =>
            {
                config.Name = "create";
                config.Description = "create budget providing year, month and currency";
                config.HelpOption(HelpFlagTemplate);

                var yearOption = config.Option("-y |--year", "year", CommandOptionType.SingleValue);
                var monthOption = config.Option("-m |--month", "month [1-12]", CommandOptionType.SingleValue);
                var currencyOption = config.Option("-c |--currency", "currency [PLN, USD]", CommandOptionType.SingleValue);
                
                config.OnExecute(() =>
                {
                    if (!yearOption.HasValue() || !monthOption.HasValue() || !currencyOption.HasValue())
                    {
                        config.ShowHelp();
                        return 1;
                    }

                    int.TryParse(monthOption.Value(), out var monthIndex);
                    int.TryParse(yearOption.Value(), out var year);

                    var month = Month.Create(year, MonthName.Create(monthIndex));
                    var baseCurrency = Currency.Create(currencyOption.Value());

                    var budget = new Budget(month, baseCurrency);
                    
                    if (BudgetNotExists(budget))
                    {
                        Save(budget, FileName.Real(budget));
                        Save(budget, FileName.Planned(budget));
                        
                        Console.WriteLine($"Budget {budget} has been created.");
                    }
                    else
                    {
                        Console.WriteLine($"Budget {budget} already exists.");
                    }
                    
                    return 0; 
                });
            }, false);    
        }

        private static bool BudgetNotExists(Budget budget) =>
            !FileName.Real(budget).Exists || !FileName.Planned(budget).Exists;

        private static void Save(Budget budget, FileName fileName) =>
            File.WriteAllText(fileName.Path, 
                new JsonBudget(budget).Content.Value);
    }
}