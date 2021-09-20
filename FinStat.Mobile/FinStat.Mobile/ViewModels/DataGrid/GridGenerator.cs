using System.Collections.Generic;
using FinStat.Domain.Enums;
using FinStat.Domain.Models;

namespace FinStat.Mobile.ViewModels.DataGrid
{
    public class GridGenerator
    {
        public IEnumerable<RowViewModel> GenerateIncomeStatements(string companyName, IList<IncomeStatement> incomeStatements, DisplayUnit displayUnit)
        {
            var rows = new List<RowViewModel>();
            foreach (var parameterDefinition in ParameterDefinitions.IncomeStatementDefinitions)
            {
                var cells = new List<CellViewModel>();
                cells.Add(new CellViewModel(companyName, parameterDefinition.Title));
                foreach (var incomeStatement in incomeStatements)
                {
                    cells.Add(new CellViewModel(incomeStatement.Date, parameterDefinition.Value(incomeStatement, displayUnit)));
                }
                rows.Add(new RowViewModel(cells));
            }

            return rows;
        }

        public IEnumerable<RowViewModel> GenerateBalanceSheetStatements(string companyName, IList<BalanceSheet> balanceSheets, DisplayUnit displayUnit)
        {
            var rows = new List<RowViewModel>();
            foreach (var parameterDefinition in ParameterDefinitions.BalanceSheetDefinitions)
            {
                var cells = new List<CellViewModel>();
                cells.Add(new CellViewModel(companyName, parameterDefinition.Title));
                foreach (var balanceSheet in balanceSheets)
                {
                    cells.Add(new CellViewModel(balanceSheet.Date, parameterDefinition.Value(balanceSheet, displayUnit)));
                }
                rows.Add(new RowViewModel(cells));
            }

            return rows;
        }
    }
}
