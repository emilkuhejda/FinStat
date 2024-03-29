﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinStat.Domain.Enums;
using FinStat.Domain.Interfaces.Configuration;
using FinStat.Domain.Interfaces.Services;
using FinStat.Domain.Models;
using FinStat.Mobile.ViewModels.DataGrid;
using Prism.Navigation;

namespace FinStat.Mobile.ViewModels
{
    public class IncomeStatementPageViewModel : StatementPageViewModelBase
    {
        private readonly IWebService _webService;
        private readonly IApplicationSettings _applicationSettings;

        public IncomeStatementPageViewModel(
            IWebService webService,
            IApplicationSettings applicationSettings,
            INavigationService navigationService)
            : base(navigationService)
        {
            _webService = webService;
            _applicationSettings = applicationSettings;
        }

        public IEnumerable<IncomeStatement> IncomeStatements { get; private set; } = Enumerable.Empty<IncomeStatement>().ToList();

        public async Task InitializeAsync(SearchResult searchResult, bool quarterlyData, DisplayUnit displayUnit)
        {
            var result = await HandleWebCallAsync(() => _webService.GetIncomeStatementsAsync(searchResult.Symbol, quarterlyData, _applicationSettings.StatementsLimit));
            if (result.success)
            {
                var gridGenerator = new GridGenerator();
                IncomeStatements = result.payload;
                Rows = gridGenerator.GenerateIncomeStatements(searchResult.Name, result.payload, displayUnit);
            }
            else
            {
                IncomeStatements = Enumerable.Empty<IncomeStatement>().ToList();
                Rows = new List<RowViewModel>();
            }
        }
    }
}
