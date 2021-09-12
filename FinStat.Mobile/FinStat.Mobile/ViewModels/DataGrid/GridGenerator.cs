using System;
using System.Collections.Generic;
using System.Globalization;
using FinStat.Business.Extensions;
using FinStat.Domain.Enums;
using FinStat.Domain.Models;
using FinStat.Resources.Localization;

namespace FinStat.Mobile.ViewModels.DataGrid
{
    public class GridGenerator
    {
        private readonly IEnumerable<ParameterDefinition> _parameterDefinitions = new List<ParameterDefinition>
        {
            new ParameterDefinition(Loc.Text(TranslationKeys.Revenue), (i, u) => FormatNumber(i.Revenue, u)),
            new ParameterDefinition(Loc.Text(TranslationKeys.CostOfGoodsSold), (i, u) => FormatNumber(i.CostOfRevenue, u)),
            new ParameterDefinition(Loc.Text(TranslationKeys.CostOfGoodsSoldRatio), (i, _) => FormatPercentage(i.CostOfRevenue / (i.Revenue * 1.0))),
        };

        private static string FormatNumber(long value, DisplayUnit displayUnit)
        {
            return (value / displayUnit.ToUnit()).ToString(CultureInfo.InvariantCulture);
        }

        private static string FormatPercentage(double value)
        {
            return $"{Math.Round(value * 10000.0) / 100}%";
        }

        public IEnumerable<RowViewModel> GenerateIncomeStatements(string companyName, IList<IncomeStatement> incomeStatements, DisplayUnit displayUnit)
        {
            var rows = new List<RowViewModel>();
            foreach (var parameterDefinition in _parameterDefinitions)
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

        private class ParameterDefinition
        {
            public ParameterDefinition(string title, Func<IncomeStatement, DisplayUnit, string> value)
            {
                Title = title;
                Value = value;
            }

            public string Title { get; }

            public Func<IncomeStatement, DisplayUnit, string> Value { get; }
        }
    }
}
