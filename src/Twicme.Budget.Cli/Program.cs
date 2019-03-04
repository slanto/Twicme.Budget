﻿using System;
using Microsoft.Extensions.CommandLineUtils;
using Twicme.Budget.Cli.CommandBuilders;
using Twicme.Budget.Cli.Options;

namespace Twicme.Budget.Cli
{
    class Program
    {   
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
            var budgetCommand = app.Command("budget", config =>
            {
                config.HelpOption(HelpFlagTemplate.Value);  
            });

            budgetCommand.Command("create", config =>
            {
                config.Name = "create";
                config.Description = "create budget providing year, month and currency";
                config.HelpOption(HelpFlagTemplate.Value);     
                
                var yearOption = YearOption.Create(config);
                var monthOption = MonthOption.Create(config);
                var currencyOption = CurrencyOption.Create(config);
                
                config.OnExecute(() =>
                {
                 if (yearOption.NotExists || monthOption.NotExists || currencyOption.NotExists)
                    {
                        config.ShowHelp();
                        return ErrorCode.Value;
                    }

                 var currency = Currency.Create(currencyOption.Value);     
                 var month = Month.Create(yearOption.Value, monthOption.Value);

                 new BudgetFile(month, currency)
                     .InPlanningMode()
                     .Store();
                 
                 new BudgetFile(month, currency)
                     .Store();
                
                    return OkCode.Value;
                });
            });
            
            budgetCommand.Command("add", config =>
            {
                config.Name = "add";
                config.Description = "add revenue or expense to budget";
                config.HelpOption(HelpFlagTemplate.Value);
                
                var yearOption = YearOption.Create(config);
                var monthOption = MonthOption.Create(config);
                var currencyOption = CurrencyOption.Create(config);
                var amountOption = AmountOption.Create(config);
                var categoryOption = CategoryOption.Create(config);
                var descriptionOption = DescriptionOption.Create(config);
                var planningModeOption = PlanningModeOption.Create(config);
                
                config.OnExecute(() =>
                {
                    if (yearOption.NotExists ||
                        monthOption.NotExists ||
                        currencyOption.NotExists ||
                        amountOption.NotExists ||
                        categoryOption.NotExists)
                    {
                        config.ShowHelp();
                        return ErrorCode.Value;
                    }

                    var category = Category.Create(categoryOption.Value);
                    var description = new Description(descriptionOption.Value);
                    var currency = Currency.Create(currencyOption.Value);
                    var amount = Amount.Create(amountOption.Value, currency);
                    var money = new Money(amount, category, DateTimeOffset.UtcNow, description);
                      
                    var month = Month.Create(yearOption.Value, monthOption.Value);
                   
                    var budgetFile = new BudgetFile(month, currency)
                        .WithMoney(money);

                    if (planningModeOption.Exists)
                    {
                        budgetFile = budgetFile.InPlanningMode();
                    }

                    budgetFile.Store();
                    
                    return OkCode.Value;
                });
                
            });

            return app.Execute(args);
        }
    }
}