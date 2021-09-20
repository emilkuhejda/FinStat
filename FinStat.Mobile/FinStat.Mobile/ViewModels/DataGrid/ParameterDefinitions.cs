using System;
using System.Collections.Generic;
using System.Globalization;
using FinStat.Business.Extensions;
using FinStat.Domain.Enums;
using FinStat.Domain.Models;
using FinStat.Resources.Localization;

namespace FinStat.Mobile.ViewModels.DataGrid
{
    public static class ParameterDefinitions
    {
        public static IEnumerable<ParameterDefinition<IncomeStatement>> IncomeStatementDefinitions { get; } =
            new List<ParameterDefinition<IncomeStatement>>
            {
                new ParameterDefinition<IncomeStatement>(
                    Loc.Text(TranslationKeys.Revenue),
                    (i, u) => FormatNumber(i.Revenue, u)),
                new ParameterDefinition<IncomeStatement>(
                    Loc.Text(TranslationKeys.CostOfGoodsSold),
                    (i, u) => FormatNumber(i.CostOfRevenue, u)),
                new ParameterDefinition<IncomeStatement>(
                    Loc.Text(TranslationKeys.CostOfGoodsSoldRatio),
                    (i, _) => FormatPercentage(i.CostOfRevenue / (i.Revenue * 1.0))),
                new ParameterDefinition<IncomeStatement>(
                    Loc.Text(TranslationKeys.GrossProfit),
                    (i, u) => FormatNumber(i.GrossProfit, u)),
                new ParameterDefinition<IncomeStatement>(
                    Loc.Text(TranslationKeys.GrossProfitRatio),
                    (i, u) => FormatPercentage(i.GrossProfitRatio)),
                new ParameterDefinition<IncomeStatement>(
                    Loc.Text(TranslationKeys.SGAExpenses),
                    (i, u) => FormatNumber(i.SellingGeneralAndAdministrativeExpenses, u)),
                new ParameterDefinition<IncomeStatement>(
                    Loc.Text(TranslationKeys.SGAExpensesRatio),
                    (i, u) => FormatPercentage(i.SellingGeneralAndAdministrativeExpenses / (i.GrossProfit * 1.0))),
                new ParameterDefinition<IncomeStatement>(
                    Loc.Text(TranslationKeys.RADExpenses),
                    (i, u) => FormatNumber(i.ResearchAndDevelopmentExpenses, u)),
                new ParameterDefinition<IncomeStatement>(
                    Loc.Text(TranslationKeys.RADExpensesRatio),
                    (i, u) => FormatPercentage(i.ResearchAndDevelopmentExpenses / (i.GrossProfit * 1.0))),
                new ParameterDefinition<IncomeStatement>(
                    Loc.Text(TranslationKeys.DepreciationAmortization),
                    (i, u) => FormatNumber(i.DepreciationAndAmortization, u)),
                new ParameterDefinition<IncomeStatement>(
                    Loc.Text(TranslationKeys.DepreciationAmortizationRatio),
                    (i, u) => FormatPercentage(i.DepreciationAndAmortization / (i.GrossProfit * 1.0))),
                new ParameterDefinition<IncomeStatement>(
                    Loc.Text(TranslationKeys.OperatingExpenses),
                    (i, u) => FormatNumber(i.OperatingExpenses, u)),
                new ParameterDefinition<IncomeStatement>(
                    Loc.Text(TranslationKeys.OperatingIncome),
                    (i, u) => FormatNumber(i.OperatingIncome, u)),
                new ParameterDefinition<IncomeStatement>(
                    Loc.Text(TranslationKeys.OperatingIncomeRatio),
                    (i, u) => FormatPercentage(i.OperatingIncomeRatio)),
                new ParameterDefinition<IncomeStatement>(
                    Loc.Text(TranslationKeys.InterestExpense),
                    (i, u) => FormatNumber(i.InterestExpense, u)),
                new ParameterDefinition<IncomeStatement>(
                    Loc.Text(TranslationKeys.InterestExpenseRatio),
                    (i, u) => FormatPercentage(i.InterestExpense / (i.OperatingIncome * 1.0))),
                new ParameterDefinition<IncomeStatement>(
                    Loc.Text(TranslationKeys.PreTaxIncome),
                    (i, u) => FormatNumber(i.IncomeBeforeTax, u)),
                new ParameterDefinition<IncomeStatement>(
                    Loc.Text(TranslationKeys.IncomeTaxes),
                    (i, u) => FormatNumber(i.IncomeTaxExpense, u)),
                new ParameterDefinition<IncomeStatement>(
                    Loc.Text(TranslationKeys.TaxesPercentage),
                    (i, u) => FormatPercentage(i.IncomeTaxExpense / (i.IncomeBeforeTax * 1.0))),
                new ParameterDefinition<IncomeStatement>(
                    Loc.Text(TranslationKeys.NetEarnings),
                    (i, u) => FormatNumber(i.NetIncome, u)),
                new ParameterDefinition<IncomeStatement>(
                    Loc.Text(TranslationKeys.NetEarningsRatio),
                    (i, u) => FormatPercentage(i.NetIncomeRatio)),
                new ParameterDefinition<IncomeStatement>(
                    Loc.Text(TranslationKeys.EPS),
                    (i, _) => FormatNumber(i.Eps, 2)),
                new ParameterDefinition<IncomeStatement>(
                    Loc.Text(TranslationKeys.EPSDiluted),
                    (i, _) => FormatNumber(i.Epsdiluted, 2))
            };

        public static IEnumerable<ParameterDefinition<BalanceSheet>> BalanceSheetDefinitions { get; } =
            new List<ParameterDefinition<BalanceSheet>>
            {
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.CashAndCashEquivalents),
                    (b, u) => FormatNumber(b.CashAndCashEquivalents, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.ShortTermInvestments),
                    (b, u) => FormatNumber(b.ShortTermInvestments, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.CashAndShortTermInvestments),
                    (b, u) => FormatNumber(b.CashAndShortTermInvestments, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.NetReceivables),
                    (b, u) => FormatNumber(b.NetReceivables, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.Inventory),
                    (b, u) => FormatNumber(b.Inventory, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.OtherCurrentAssets),
                    (b, u) => FormatNumber(b.OtherCurrentAssets, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.TotalCurrentAssets),
                    (b, u) => FormatNumber(b.TotalCurrentAssets, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.NetPropertyPlantEquipment),
                    (b, u) => FormatNumber(b.PropertyPlantEquipmentNet, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.Goodwill),
                    (b, u) => FormatNumber(b.Goodwill, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.IntangibleAssets),
                    (b, u) => FormatNumber(b.IntangibleAssets, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.GoodwillAndIntangibleAssets),
                    (b, u) => FormatNumber(b.GoodwillAndIntangibleAssets, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.LongTermInvestments),
                    (b, u) => FormatNumber(b.LongTermInvestments, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.TaxAssets),
                    (b, u) => FormatNumber(b.TaxAssets, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.OtherNonCurrentAssets),
                    (b, u) => FormatNumber(b.OtherNonCurrentAssets, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.TotalNonCurrentAssets),
                    (b, u) => FormatNumber(b.TotalNonCurrentAssets, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.OtherAssets),
                    (b, u) => FormatNumber(b.OtherAssets, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.TotalAssets),
                    (b, u) => FormatNumber(b.TotalAssets, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.AccountPayables),
                    (b, u) => FormatNumber(b.AccountPayables, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.ShortTermDebt),
                    (b, u) => FormatNumber(b.ShortTermDebt, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.TaxPayables),
                    (b, u) => FormatNumber(b.TaxPayables, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.DeferredRevenue),
                    (b, u) => FormatNumber(b.DeferredRevenue, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.OtherCurrentLiabilities),
                    (b, u) => FormatNumber(b.OtherCurrentLiabilities, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.TotalCurrentLiabilities),
                    (b, u) => FormatNumber(b.TotalCurrentLiabilities, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.LongTermDebt),
                    (b, u) => FormatNumber(b.LongTermDebt, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.NonCurrentDeferredRevenue),
                    (b, u) => FormatNumber(b.DeferredRevenueNonCurrent, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.NonCurrentDeferredTaxLiabilities),
                    (b, u) => FormatNumber(b.DeferredTaxLiabilitiesNonCurrent, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.OtherNonCurrentLiabilities),
                    (b, u) => FormatNumber(b.OtherNonCurrentLiabilities, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.TotalNonCurrentLiabilities),
                    (b, u) => FormatNumber(b.TotalNonCurrentLiabilities, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.OtherLiabilities),
                    (b, u) => FormatNumber(b.OtherLiabilities, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.TotalLiabilities),
                    (b, u) => FormatNumber(b.TotalLiabilities, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.RetainedEarnings),
                    (b, u) => FormatNumber(b.RetainedEarnings, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.AccumulatedOtherComprehensiveIncomeLoss),
                    (b, u) => FormatNumber(b.AccumulatedOtherComprehensiveIncomeLoss, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.OtherTotalStockholdersEquity),
                    (b, u) => FormatNumber(b.OtherTotalStockholdersEquity, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.TotalStockholdersEquity),
                    (b, u) => FormatNumber(b.TotalStockholdersEquity, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.TotalLiabilitiesAndStockholdersEquity),
                    (b, u) => FormatNumber(b.TotalLiabilitiesAndStockholdersEquity, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.TotalInvestments),
                    (b, u) => FormatNumber(b.TotalInvestments, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.TotalDebt),
                    (b, u) => FormatNumber(b.TotalDebt, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.NetDebt),
                    (b, u) => FormatNumber(b.NetDebt, u)),
                new ParameterDefinition<BalanceSheet>(
                    Loc.Text(TranslationKeys.CommonStock),
                    (b, u) => FormatNumber(b.CommonStock, u))
            };

        private static string FormatNumber(double value, DisplayUnit displayUnit)
        {
            return FormatNumber(Convert.ToInt64(value), displayUnit);
        }

        private static string FormatNumber(long value, DisplayUnit displayUnit)
        {
            return (value / displayUnit.ToUnit()).ToString("N0", CultureInfo.InvariantCulture);
        }

        private static string FormatNumber(double value, uint decimals)
        {
            if (decimals == 0)
                return Math.Round(value).ToString("N0", CultureInfo.InvariantCulture);

            return (Math.Round(value * Math.Pow(10, decimals)) / Math.Pow(10, decimals)).ToString($"N{decimals}", CultureInfo.InvariantCulture);
        }

        private static string FormatPercentage(double value)
        {
            return $"{Math.Round(value * 10000.0) / 100}%";
        }
    }
}
