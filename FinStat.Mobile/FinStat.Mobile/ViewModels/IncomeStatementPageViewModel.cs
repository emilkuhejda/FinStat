using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinStat.Common.Utils;
using FinStat.Domain.Interfaces.Configuration;
using FinStat.Domain.Interfaces.Services;
using FinStat.Domain.Models;
using FinStat.Mobile.Extensions;
using FinStat.Mobile.ViewModels.DataGrid;
using FinStat.Resources.Localization;
using Prism.Navigation;

namespace FinStat.Mobile.ViewModels
{
    public class IncomeStatementPageViewModel : ViewModelBase
    {
        private readonly IWebService _webService;
        private readonly IApplicationSettings _applicationSettings;

        private IEnumerable<RowViewModel> _rows;
        private string _displayUnitsText;

        public IncomeStatementPageViewModel(
            IWebService webService,
            IApplicationSettings applicationSettings,
            INavigationService navigationService)
            : base(navigationService)
        {
            _webService = webService;
            _applicationSettings = applicationSettings;

            CanGoBack = true;
            HasTitleBar = true;
            HasBottomNavigation = true;

            DisplayUnitsText = Loc.Text(TranslationKeys.AllNumbersInUnit, Loc.Text(_applicationSettings.DisplayUnit));
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

        public string DisplayUnitsText
        {
            get => _displayUnitsText;
            set => SetProperty(ref _displayUnitsText, value);
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
                    Rows = gridGenerator.GenerateIncomeStatements(searchResult.Name, result.payload, _applicationSettings.DisplayUnit);
                }
            }
        }
    }
}
