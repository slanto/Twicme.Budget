using FluentAssertions;
using Xunit;

namespace Twicme.Budget.Tests
{
    public class TotalRevenueTests
    {
        private readonly Budget _budget;

        public TotalRevenueTests()
        {
            _budget = new BudgetTestDataBuilder().Build();
        }

        [Fact]
        public void GivenBudget_WhenTotalRevenueIsCalled_ThenTotalAmountIsCalculated()
        {
            var totalRevenue = new TotalRevenue(_budget);
            totalRevenue.Value.Should().Be(Amount.Create(2250.55M, _budget.BaseCurrency));
        }
    }
    
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
        public void GivenBudget_WhenTotalExpenseIsCalled_ThenTotalAmountIsCalculated()
        {   
            var totalExpense = _financialReport.TotalExpense();
            
            totalExpense.Should().Be(Amount.Create(-101.10M, _budget.BaseCurrency));
        }
    }
}