using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinStat.Domain.Interfaces.Configuration;
using FinStat.Domain.Interfaces.Services;
using FinStat.Domain.Models;
using FinStat.Mobile.ViewModels.DataGrid;
using Prism.Navigation;

namespace FinStat.Mobile.ViewModels
{
    public class IncomeStatementPageViewModel : ViewModelBase
    {
        private readonly IWebService _webService;
        private readonly IApplicationSettings _applicationSettings;

        private IEnumerable<RowViewModel> _rows;

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

        public bool NoDataToPlot => Rows == null || !Rows.Any();

        public IEnumerable<RowViewModel> Rows
        {
            get => _rows;
            set
            {
                if (SetProperty(ref _rows, value))
                {
                    RaisePropertyChanged(nameof(NoDataToPlot));
                }
            }
        }

        public async Task InitializeAsync(SearchResult searchResult, bool quarterlyData)
        {
            var result = await HandleWebCallAsync(() => _webService.GetIncomeStatementsAsync(searchResult.Symbol, quarterlyData, _applicationSettings.StatementsLimit));
            if (result.success)
            {
                var gridGenerator = new GridGenerator();
                IncomeStatements = result.payload;
                Rows = gridGenerator.GenerateIncomeStatements(searchResult.Name, result.payload, _applicationSettings.DisplayUnit);
            }
            else
            {
                IncomeStatements = Enumerable.Empty<IncomeStatement>().ToList();
                Rows = new List<RowViewModel>();
            }
        }
    }
}
