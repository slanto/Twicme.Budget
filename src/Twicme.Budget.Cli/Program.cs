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
            var budget = app.Command("budget", config =>
            {
                config.HelpOption(HelpFlagTemplate.Value);  
            });

            budget.Command("create", config =>
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

                    var budgetFilesFactory = new BudgetFilesFactory();
                    var (plannedBudgetFile, realBudgetFile) = budgetFilesFactory.Create(yearOption, monthOption, currencyOption);
            
                    if (plannedBudgetFile.NotExists || realBudgetFile.NotExists)
                    {
                        plannedBudgetFile.Save();
                        realBudgetFile.Save();
                
                        Console.WriteLine($"Budget {plannedBudgetFile.Budget} has been created.");
                        return OkCode.Value;
                    }
            
                    Console.WriteLine($"Budget {plannedBudgetFile.Budget} already exists.");
                    
                    return OkCode.Value;
                });
            });
            
            budget.Command("add", config =>
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
                
                config.OnExecute(() =>
                {
                    if (yearOption.NotExists ||
                        monthOption.NotExists ||
                        currencyOption.NotExists ||
                        amountOption.NotExists ||
                        categoryOption.NotExists ||
                        descriptionOption.NotExists)
                    {
                        config.ShowHelp();
                        return ErrorCode.Value;
                    }
                    
                    var budgetFilesFactory = new BudgetFilesFactory();
                    var (plannedBudgetFile, realBudgetFile) = budgetFilesFactory.Create(yearOption, monthOption, currencyOption);
            
                    if (realBudgetFile.NotExists)
                    {
                        Console.WriteLine($"Budget {realBudgetFile.Budget} does not exit. Created new budget first.");
                        return ErrorCode.Value;
                    }

                    var budgetFile = realBudgetFile.Load();

                    var category = Category.Create(categoryOption.Value);
                    var description = new Description(descriptionOption.Value);
                    var currency = Currency.Create(currencyOption.Value);
                    var amount = Amount.Create(amountOption.Value, currency);
      
                    var newBudget = budgetFile.Budget.WithExpense(new Money(amount, category, DateTimeOffset.UtcNow, description));
                    var newBudgetFile = new BudgetFile(newBudget, budgetFile.FileName);
                    
                    newBudgetFile.Save();
                    
                    Console.WriteLine("Expense added.");
                    return OkCode.Value;
                });
                
            });
            

//            var budget = new BudgetCommandBuilder(app).Build();
//            
//            new CreateBudgetCommandBuilder(budget).Build();
//            new BudgetsListCommandBuilder(budget).Build();
//            new AddCommandBuilder(budget).Build();

            return app.Execute(args);
        }
    }
}