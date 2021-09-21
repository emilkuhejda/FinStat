﻿using System;
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
        private const string EmptyFieldValue = "-";

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
                    (i, _) => FormatNumber(i.Epsdiluted, 2)),
            };

        public static IEnumerable<ParameterDefinition<BalanceSheetWrapper>> BalanceSheetDefinitions { get; } =
            new List<ParameterDefinition<BalanceSheetWrapper>>
            {
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.CashAndCashEquivalents),
                    (b, u) => FormatNumber(b.BalanceSheet.CashAndCashEquivalents, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.ShortTermInvestments),
                    (b, u) => FormatNumber(b.BalanceSheet.ShortTermInvestments, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.CashAndShortTermInvestments),
                    (b, u) => FormatNumber(b.BalanceSheet.CashAndShortTermInvestments, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.NetReceivables),
                    (b, u) => FormatNumber(b.BalanceSheet.NetReceivables, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.ReceivablesRatio),
                    (b, u) => HandleValue(
                        () => b.IncomeStatement == null,
                        () => FormatPercentage(b.BalanceSheet.NetReceivables / (b.IncomeStatement.GrossProfit * 1.0)))),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.Inventory),
                    (b, u) => FormatNumber(b.BalanceSheet.Inventory, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.OtherCurrentAssets),
                    (b, u) => FormatNumber(b.BalanceSheet.OtherCurrentAssets, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.TotalCurrentAssets),
                    (b, u) => FormatNumber(b.BalanceSheet.TotalCurrentAssets, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.NetPropertyPlantEquipment),
                    (b, u) => FormatNumber(b.BalanceSheet.PropertyPlantEquipmentNet, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.Goodwill),
                    (b, u) => FormatNumber(b.BalanceSheet.Goodwill, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.IntangibleAssets),
                    (b, u) => FormatNumber(b.BalanceSheet.IntangibleAssets, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.GoodwillAndIntangibleAssets),
                    (b, u) => FormatNumber(b.BalanceSheet.GoodwillAndIntangibleAssets, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.LongTermInvestments),
                    (b, u) => FormatNumber(b.BalanceSheet.LongTermInvestments, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.TaxAssets),
                    (b, u) => FormatNumber(b.BalanceSheet.TaxAssets, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.OtherNonCurrentAssets),
                    (b, u) => FormatNumber(b.BalanceSheet.OtherNonCurrentAssets, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.TotalNonCurrentAssets),
                    (b, u) => FormatNumber(b.BalanceSheet.TotalNonCurrentAssets, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.OtherAssets),
                    (b, u) => FormatNumber(b.BalanceSheet.OtherAssets, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.TotalAssets),
                    (b, u) => FormatNumber(b.BalanceSheet.TotalAssets, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.AccountPayables),
                    (b, u) => FormatNumber(b.BalanceSheet.AccountPayables, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.ShortTermDebt),
                    (b, u) => FormatNumber(b.BalanceSheet.ShortTermDebt, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.TaxPayables),
                    (b, u) => FormatNumber(b.BalanceSheet.TaxPayables, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.DeferredRevenue),
                    (b, u) => FormatNumber(b.BalanceSheet.DeferredRevenue, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.OtherCurrentLiabilities),
                    (b, u) => FormatNumber(b.BalanceSheet.OtherCurrentLiabilities, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.TotalCurrentLiabilities),
                    (b, u) => FormatNumber(b.BalanceSheet.TotalCurrentLiabilities, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.LongTermDebt),
                    (b, u) => FormatNumber(b.BalanceSheet.LongTermDebt, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.NonCurrentDeferredRevenue),
                    (b, u) => FormatNumber(b.BalanceSheet.DeferredRevenueNonCurrent, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.NonCurrentDeferredTaxLiabilities),
                    (b, u) => FormatNumber(b.BalanceSheet.DeferredTaxLiabilitiesNonCurrent, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.OtherNonCurrentLiabilities),
                    (b, u) => FormatNumber(b.BalanceSheet.OtherNonCurrentLiabilities, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.TotalNonCurrentLiabilities),
                    (b, u) => FormatNumber(b.BalanceSheet.TotalNonCurrentLiabilities, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.OtherLiabilities),
                    (b, u) => FormatNumber(b.BalanceSheet.OtherLiabilities, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.TotalLiabilities),
                    (b, u) => FormatNumber(b.BalanceSheet.TotalLiabilities, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.RetainedEarnings),
                    (b, u) => FormatNumber(b.BalanceSheet.RetainedEarnings, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.RetainedEarningsRatio),
                    (b, u) => HandleValue(
                        () => b.RecentBalanceSheet == null,
                        () => FormatPercentage(b.BalanceSheet.RetainedEarnings /
                                               (b.RecentBalanceSheet.RetainedEarnings * 1.0)))),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.AccumulatedOtherComprehensiveIncomeLoss),
                    (b, u) => FormatNumber(b.BalanceSheet.AccumulatedOtherComprehensiveIncomeLoss, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.OtherTotalStockholdersEquity),
                    (b, u) => FormatNumber(b.BalanceSheet.OtherTotalStockholdersEquity, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.TotalStockholdersEquity),
                    (b, u) => FormatNumber(b.BalanceSheet.TotalStockholdersEquity, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.TotalLiabilitiesAndStockholdersEquity),
                    (b, u) => FormatNumber(b.BalanceSheet.TotalLiabilitiesAndStockholdersEquity, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.TotalInvestments),
                    (b, u) => FormatNumber(b.BalanceSheet.TotalInvestments, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.TotalDebt),
                    (b, u) => FormatNumber(b.BalanceSheet.TotalDebt, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.NetDebt),
                    (b, u) => FormatNumber(b.BalanceSheet.NetDebt, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.TheCurrentRatio),
                    (b, u) => FormatNumber(
                        b.BalanceSheet.TotalCurrentAssets / (b.BalanceSheet.TotalCurrentLiabilities * 1.0), 2)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.TheReturnOnTotalAssets),
                    (b, u) => HandleValue(
                        () => b.IncomeStatement == null,
                        () => FormatPercentage(b.IncomeStatement.NetIncome / (b.BalanceSheet.TotalAssets * 1.0)))),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.ShareholderEquityRatio),
                    (b, u) => FormatNumber(
                        b.BalanceSheet.TotalLiabilities / (b.BalanceSheet.TotalStockholdersEquity * 1.0), 2)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.ReturnOnShareholdersEquity),
                    (b, u) => HandleValue(
                        () => b.IncomeStatement == null,
                        () => FormatPercentage(b.IncomeStatement.NetIncome /
                                               (b.BalanceSheet.TotalStockholdersEquity * 1.0)))),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.CommonStock),
                    (b, u) => FormatNumber(b.BalanceSheet.CommonStock, u)),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.PreTaxEarningPerShare),
                    (b, u) => HandleValue(
                        () => b.IncomeStatement == null,
                        () => FormatNumber(b.IncomeStatement.IncomeBeforeTax / (b.BalanceSheet.CommonStock * 1.0), 2))),
                new ParameterDefinition<BalanceSheetWrapper>(
                    Loc.Text(TranslationKeys.PreTaxEarningPerShare),
                    (b, u) => HandleValue(
                        () => b.IncomeStatement == null,
                        () => FormatNumber(b.IncomeStatement.NetIncome / (b.BalanceSheet.CommonStock * 1.0), 2))),
            };

        public static IEnumerable<ParameterDefinition<CashFlowWrapper>> CashFlowDefinitions { get; } =
            new List<ParameterDefinition<CashFlowWrapper>>
            {
                new ParameterDefinition<CashFlowWrapper>(
                    "Net income",
                    (c, u) => FormatNumber(c.CashFlow.NetIncome, u)),
            };

        private static string HandleValue(Func<bool> conditionFunc, Func<string> defaultValueFunc)
        {
            if (conditionFunc())
                return EmptyFieldValue;

            return defaultValueFunc();
        }

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
