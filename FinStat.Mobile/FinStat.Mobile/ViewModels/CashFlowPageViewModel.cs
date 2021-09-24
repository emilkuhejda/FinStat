using System.Collections.Generic;
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
    public class CashFlowPageViewModel : StatementPageViewModelBase
    {
        private readonly IWebService _webService;
        private readonly IApplicationSettings _applicationSettings;

        private bool _isInitialized;

        public CashFlowPageViewModel(
            IWebService webService,
            IApplicationSettings applicationSettings,
            INavigationService navigationService)
            : base(navigationService)
        {
            _webService = webService;
            _applicationSettings = applicationSettings;
        }

        public async Task InitializeAsync(IEnumerable<IncomeStatement> incomeStatements, SearchResult searchResult, bool quarterlyData, DisplayUnit displayUnit)
        {
            if (_isInitialized)
                return;

            var result = await HandleWebCallAsync(() => _webService.GetCashFlowStatementsAsync(searchResult.Symbol, quarterlyData, _applicationSettings.StatementsLimit));
            if (result.success)
            {
                var gridGenerator = new GridGenerator();
                Rows = gridGenerator.GenerateCashFlowStatements(searchResult.Name, result.payload, incomeStatements.ToList(), displayUnit);
                _isInitialized = true;
            }
            else
            {
                Rows = new List<RowViewModel>();
            }
        }

        public void ClearData()
        {
            _isInitialized = false;
            Rows = new List<RowViewModel>();
        }
    }
}
