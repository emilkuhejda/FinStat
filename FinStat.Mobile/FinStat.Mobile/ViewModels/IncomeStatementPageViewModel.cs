using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinStat.Common.Utils;
using FinStat.Domain.Interfaces.Services;
using FinStat.Domain.Models;
using FinStat.Mobile.Extensions;
using FinStat.Mobile.ViewModels.DataGrid;
using Prism.Navigation;

namespace FinStat.Mobile.ViewModels
{
    public class IncomeStatementPageViewModel : ViewModelBase
    {
        private readonly IWebService _webService;

        private IEnumerable<RowViewModel> _rows;

        public IncomeStatementPageViewModel(
            IWebService webService,
            INavigationService navigationService)
            : base(navigationService)
        {
            _webService = webService;

            CanGoBack = true;
            HasTitleBar = true;
            HasBottomNavigation = true;
        }

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

        protected override async Task LoadDataAsync(INavigationParameters navigationParameters)
        {
            using (new OperationMonitor(OperationScope))
            {
                var searchResult = navigationParameters.GetValue<SearchResult>();
                Title = searchResult.Name;

                var result = await HandleWebCallAsync(() => _webService.GetIncomeStatementsAsync(searchResult.Symbol, false));
                if (result.success)
                {
                    var gridGenerator = new GridGenerator();
                    Rows = gridGenerator.GenerateIncomeStatements(searchResult.Name, result.payload);
                }
            }
        }
    }
}
