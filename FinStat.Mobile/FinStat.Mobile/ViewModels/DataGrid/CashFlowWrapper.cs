using FinStat.Domain.Models;

namespace FinStat.Mobile.ViewModels.DataGrid
{
    public class CashFlowWrapper
    {
        public CashFlowWrapper(CashFlow cashFlow, IncomeStatement incomeStatement)
        {
            CashFlow = cashFlow;
            IncomeStatement = incomeStatement;
        }

        public CashFlow CashFlow { get; }

        public IncomeStatement IncomeStatement { get; }
    }
}
