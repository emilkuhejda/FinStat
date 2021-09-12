using System;
using System.Collections.Generic;
using System.Globalization;
using FinStat.Domain.Models;
using FinStat.Resources.Localization;

namespace FinStat.Mobile.ViewModels.DataGrid
{
    public class GridGenerator
    {
        private readonly IEnumerable<RowDefinition> _rowDefinitions = new List<RowDefinition>
        {
            new RowDefinition(Loc.Text(TranslationKeys.Revenue), i => FormatNumber(i.Revenue)),
            new RowDefinition(Loc.Text(TranslationKeys.CostOfGoodsSold), i => FormatNumber(i.CostOfRevenue)),
            new RowDefinition(Loc.Text(TranslationKeys.CostOfGoodsSoldRatio), i => FormatPercentage(i.CostOfRevenue / (i.Revenue * 1.0))),
        };

        private static string FormatNumber(long value)
        {
            return (value / 1000000).ToString(CultureInfo.InvariantCulture);
        }

        private static string FormatPercentage(double value)
        {
            return $"{Math.Round(value * 10000.0) / 100}%";
        }

        public IEnumerable<RowViewModel> GenerateIncomeStatements(string companyName, IList<IncomeStatement> incomeStatements)
        {
            var rows = new List<RowViewModel>();
            foreach (var rowDefinition in _rowDefinitions)
            {
                var cells = new List<CellViewModel>();
                cells.Add(new CellViewModel(companyName, rowDefinition.Title));
                foreach (var incomeStatement in incomeStatements)
                {
                    cells.Add(new CellViewModel(incomeStatement.Date, rowDefinition.Value(incomeStatement)));
                }
                rows.Add(new RowViewModel(cells));
            }

            return rows;
        }

        private class RowDefinition
        {
            public RowDefinition(string title, Func<IncomeStatement, string> value)
            {
                Title = title;
                Value = value;
            }

            public string Title { get; }

            public Func<IncomeStatement, string> Value { get; }
        }
    }
}
