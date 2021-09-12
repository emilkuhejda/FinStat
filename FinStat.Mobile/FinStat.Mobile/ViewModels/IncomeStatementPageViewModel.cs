using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FinStat.Common.Utils;
using FinStat.Domain.Interfaces.Configuration;
using FinStat.Domain.Interfaces.Services;
using FinStat.Domain.Models;
using FinStat.Mobile.Commands;
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
        private bool _annualData;
        private bool _quarterlyData;
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

            AnnualData = true;
            QuarterlyData = false;

            DisplayUnitsText = Loc.Text(TranslationKeys.AllNumbersInUnit, Loc.Text(_applicationSettings.DisplayUnit));

            LoadAnnualDataCommand = new AsyncCommand(ExecuteLoadAnnualDataCommandAsync);
            LoadQuarterlyDataCommand = new AsyncCommand(ExecuteLoadQuarterlyDataCommandAsync);
        }

        private SearchResult SearchResult { get; set; }

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

        public bool AnnualData
        {
            get => _annualData;
            set => SetProperty(ref _annualData, value);
        }

        public bool QuarterlyData
        {
            get => _quarterlyData;
            set => SetProperty(ref _quarterlyData, value);
        }

        public string DisplayUnitsText
        {
            get => _displayUnitsText;
            set => SetProperty(ref _displayUnitsText, value);
        }

        public ICommand LoadAnnualDataCommand { get; }

        public ICommand LoadQuarterlyDataCommand { get; }

        protected override Task LoadDataAsync(INavigationParameters navigationParameters)
        {
            SearchResult = navigationParameters.GetValue<SearchResult>();
            Title = SearchResult.Name;

            return InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            using (new OperationMonitor(OperationScope))
            {
                var result = await HandleWebCallAsync(() => _webService.GetIncomeStatementsAsync(SearchResult.Symbol, QuarterlyData));
                if (result.success)
                {
                    var gridGenerator = new GridGenerator();
                    Rows = gridGenerator.GenerateIncomeStatements(SearchResult.Name, result.payload, _applicationSettings.DisplayUnit);
                }
                else
                {
                    Rows = new List<RowViewModel>();
                }
            }
        }

        private Task ExecuteLoadAnnualDataCommandAsync()
        {
            if (!AnnualData)
            {
                AnnualData = true;
                return Task.CompletedTask;
            }

            QuarterlyData = false;
            return InitializeAsync();
        }

        private Task ExecuteLoadQuarterlyDataCommandAsync()
        {
            if (!QuarterlyData)
            {
                QuarterlyData = true;
                return Task.CompletedTask;
            }

            AnnualData = false;
            return InitializeAsync();
        }
    }
}
