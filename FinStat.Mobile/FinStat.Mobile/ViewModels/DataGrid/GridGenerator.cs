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
            new ParameterDefinition(
                Loc.Text(TranslationKeys.Revenue),
                (i, u) => FormatNumber(i.Revenue, u)),
            new ParameterDefinition(
                Loc.Text(TranslationKeys.CostOfGoodsSold),
                (i, u) => FormatNumber(i.CostOfRevenue, u)),
            new ParameterDefinition(
                Loc.Text(TranslationKeys.CostOfGoodsSoldRatio),
                (i, _) => FormatPercentage(i.CostOfRevenue / (i.Revenue * 1.0))),
            new ParameterDefinition(
                Loc.Text(TranslationKeys.GrossProfit),
                (i, u) => FormatNumber(i.GrossProfit, u)),
            new ParameterDefinition(
                Loc.Text(TranslationKeys.GrossProfitRatio),
                (i, u) => FormatPercentage(i.GrossProfitRatio)),
            new ParameterDefinition(
                Loc.Text(TranslationKeys.SGAExpenses),
                (i, u) => FormatNumber(i.SellingGeneralAndAdministrativeExpenses, u)),
            new ParameterDefinition(
                Loc.Text(TranslationKeys.SGAExpensesRatio),
                (i, u) => FormatPercentage(i.SellingGeneralAndAdministrativeExpenses / (i.GrossProfit * 1.0))),
            new ParameterDefinition(
                Loc.Text(TranslationKeys.RADExpenses),
                (i, u) => FormatNumber(i.ResearchAndDevelopmentExpenses, u)),
            new ParameterDefinition(
                Loc.Text(TranslationKeys.RADExpensesRatio),
                (i, u) => FormatPercentage(i.ResearchAndDevelopmentExpenses / (i.GrossProfit * 1.0))),
            new ParameterDefinition(
                Loc.Text(TranslationKeys.DepreciationAmortization),
                (i, u) => FormatNumber(i.DepreciationAndAmortization, u)),
            new ParameterDefinition(
                Loc.Text(TranslationKeys.DepreciationAmortizationRatio),
                (i, u) => FormatPercentage(i.DepreciationAndAmortization / (i.GrossProfit * 1.0))),
            new ParameterDefinition(
                Loc.Text(TranslationKeys.OperatingExpenses),
                (i, u) => FormatNumber(i.OperatingExpenses, u)),
            new ParameterDefinition(
                Loc.Text(TranslationKeys.OperatingIncome),
                (i, u) => FormatNumber(i.OperatingIncome, u)),
            new ParameterDefinition(
                Loc.Text(TranslationKeys.OperatingIncomeRatio),
                (i, u) => FormatPercentage(i.OperatingIncomeRatio)),
            new ParameterDefinition(
                Loc.Text(TranslationKeys.InterestExpense),
                (i, u) => FormatNumber(i.InterestExpense, u)),
            new ParameterDefinition(
                Loc.Text(TranslationKeys.InterestExpenseRatio),
                (i, u) => FormatPercentage(i.InterestExpense / (i.OperatingIncome * 1.0))),
            new ParameterDefinition(
                Loc.Text(TranslationKeys.PreTaxIncome),
                (i, u) => FormatNumber(i.IncomeBeforeTax, u)),
            new ParameterDefinition(
                Loc.Text(TranslationKeys.IncomeTaxes),
                (i, u) => FormatNumber(i.IncomeTaxExpense, u)),
            new ParameterDefinition(
                Loc.Text(TranslationKeys.TaxesPercentage),
                (i, u) => FormatPercentage(i.IncomeTaxExpense / (i.IncomeBeforeTax * 1.0))),
            new ParameterDefinition(
                Loc.Text(TranslationKeys.NetEarnings),
                (i, u) => FormatNumber(i.NetIncome, u)),
            new ParameterDefinition(
                Loc.Text(TranslationKeys.NetEarningsRatio),
                (i, u) => FormatPercentage(i.NetIncomeRatio)),
            new ParameterDefinition(
                Loc.Text(TranslationKeys.EPS),
                (i, _) => FormatNumber(i.Eps, 2)),
            new ParameterDefinition(
                Loc.Text(TranslationKeys.EPSDiluted),
                (i, _) => FormatNumber(i.Epsdiluted, 2))
        };

        private static string FormatNumber(long value, DisplayUnit displayUnit)
        {
            return (value / displayUnit.ToUnit()).ToString(CultureInfo.InvariantCulture);
        }

        private static string FormatNumber(double value, uint decimals)
        {
            if (decimals == 0)
                return Math.Round(value).ToString(CultureInfo.InvariantCulture);

            return (Math.Round(value * Math.Pow(10, decimals)) / Math.Pow(10, decimals)).ToString(CultureInfo.InvariantCulture);
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
