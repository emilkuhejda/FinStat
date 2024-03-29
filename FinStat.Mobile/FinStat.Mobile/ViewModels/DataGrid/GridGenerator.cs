﻿using System;
using System.Collections.Generic;
using System.Linq;
using FinStat.Domain.Enums;
using FinStat.Domain.Models;

namespace FinStat.Mobile.ViewModels.DataGrid
{
    public class GridGenerator
    {
        private const string DateFormat = "dd-MM-yyyy";

        public IEnumerable<RowViewModel> GenerateIncomeStatements(string companyName, IList<IncomeStatement> incomeStatements, DisplayUnit displayUnit)
        {
            var rows = new List<RowViewModel>();
            foreach (var parameterDefinition in ParameterDefinitions.IncomeStatementDefinitions)
            {
                var cells = new List<CellViewModel>();
                cells.Add(new CellViewModel(companyName, parameterDefinition.Title));
                foreach (var incomeStatement in incomeStatements)
                {
                    cells.Add(new CellViewModel(incomeStatement.Date.ToString(DateFormat), parameterDefinition.Value(incomeStatement, displayUnit)));
                }

                rows.Add(new RowViewModel(cells));
            }

            return rows;
        }

        public IEnumerable<RowViewModel> GenerateBalanceSheetStatements(string companyName, IList<BalanceSheet> balanceSheets, IList<IncomeStatement> incomeStatements, DisplayUnit displayUnit)
        {
            var rows = new List<RowViewModel>();
            foreach (var parameterDefinition in ParameterDefinitions.BalanceSheetDefinitions)
            {
                var cells = new List<CellViewModel>();
                cells.Add(new CellViewModel(companyName, parameterDefinition.Title));
                foreach (var balanceSheet in balanceSheets)
                {
                    var recentBalanceSheet = balanceSheets.FirstOrDefault(x => x.FillingDate < balanceSheet.FillingDate);
                    var incomeStatement = incomeStatements.SingleOrDefault(x => balanceSheet.FillingDate.Equals(x.FillingDate));
                    var balanceSheetWrapper = new BalanceSheetWrapper(balanceSheet, recentBalanceSheet, incomeStatement);
                    cells.Add(new CellViewModel(balanceSheet.Date.ToString(DateFormat), parameterDefinition.Value(balanceSheetWrapper, displayUnit)));
                }

                rows.Add(new RowViewModel(cells));
            }

            return rows;
        }

        public IEnumerable<RowViewModel> GenerateCashFlowStatements(string companyName, IList<CashFlow> cashFlows, IList<IncomeStatement> incomeStatements, DisplayUnit displayUnit)
        {
            var rows = new List<RowViewModel>();
            foreach (var parameterDefinition in ParameterDefinitions.CashFlowDefinitions)
            {
                var cells = new List<CellViewModel>();
                cells.Add(new CellViewModel(companyName, parameterDefinition.Title));
                foreach (var cashFlow in cashFlows)
                {
                    var incomeStatement = incomeStatements.SingleOrDefault(x => Convert.ToDateTime(cashFlow.FillingDate).Equals(Convert.ToDateTime(x.FillingDate)));
                    var cashFlowWrapper = new CashFlowWrapper(cashFlow, incomeStatement);
                    cells.Add(new CellViewModel(cashFlow.Date.ToString(DateFormat), parameterDefinition.Value(cashFlowWrapper, displayUnit)));
                }

                rows.Add(new RowViewModel(cells));
            }

            return rows;
        }
    }
}
