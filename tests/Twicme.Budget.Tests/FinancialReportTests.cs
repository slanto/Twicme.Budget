using FluentAssertions;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class FinancialReportTests
    {
        private readonly FinancialReport _financialReport;
        private readonly Budget _budget;

        public FinancialReportTests()
        {
            _budget = new BudgetTestDataBuilder().Build();
            _financialReport = new FinancialReport(_budget);
        }
        
        [Fact]
        public void GivenBudget_WhenTotalRevenueIsCalled_ThenTotalAmountIsCalculated()
        {
            var totalRevenue = _financialReport.TotalRevenue();
            
            totalRevenue.Should().Be(Amount.Create(2250.55M, _budget.BaseCurrency));
        }
        
        [Fact]
        public void GivenBudget_WhenTotalExpenseIsCalled_ThenTotalAmountIsCalculated()
        {   
            var totalExpense = _financialReport.TotalExpense();
            
            totalExpense.Should().Be(Amount.Create(-101.10M, _budget.BaseCurrency));
        }

        [Fact]
        public void GivenBudget_WhenBalanceIsCalled_ThenTotalAmountIsCalculated()
        {
            var totalBalance = _financialReport.TotalBalance();
            
            totalBalance.Should().Be(Amount.Create(2149.45M, _budget.BaseCurrency));
        }
    }
}