using FinStat.Domain.Models;

namespace FinStat.Mobile.ViewModels.DataGrid
{
    public class BalanceSheetWrapper
    {
        public BalanceSheetWrapper(BalanceSheet balanceSheet, BalanceSheet recentBalanceSheet, IncomeStatement incomeStatement)
        {
            BalanceSheet = balanceSheet;
            RecentBalanceSheet = recentBalanceSheet;
            IncomeStatement = incomeStatement;
        }

        public BalanceSheet BalanceSheet { get; }

        public BalanceSheet RecentBalanceSheet { get; }

        public IncomeStatement IncomeStatement { get; }
    }
}
