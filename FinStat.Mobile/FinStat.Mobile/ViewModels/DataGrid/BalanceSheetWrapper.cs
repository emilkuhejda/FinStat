using FinStat.Domain.Models;

namespace FinStat.Mobile.ViewModels.DataGrid
{
    public class BalanceSheetWrapper
    {
        public BalanceSheetWrapper(BalanceSheet balanceSheet, IncomeStatement incomeStatement)
        {
            BalanceSheet = balanceSheet;
            IncomeStatement = incomeStatement;
        }

        public BalanceSheet BalanceSheet { get; }

        public IncomeStatement IncomeStatement { get; }
    }
}
