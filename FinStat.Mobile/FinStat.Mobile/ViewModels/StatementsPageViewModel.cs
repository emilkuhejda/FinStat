using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FinStat.Business.Extensions;
using FinStat.Common.Utils;
using FinStat.Domain.Configuration;
using FinStat.Domain.Enums;
using FinStat.Domain.Interfaces.Configuration;
using FinStat.Domain.Interfaces.Repositories;
using FinStat.Domain.Interfaces.Services;
using FinStat.Domain.Models;
using FinStat.Mobile.Commands;
using FinStat.Mobile.Extensions;
using FinStat.Resources.Localization;
using Prism.Navigation;

namespace FinStat.Mobile.ViewModels
{
    public class StatementsPageViewModel : ViewModelBase
    {
        private readonly IWebService _webService;
        private readonly IInternalValueService _internalValueService;
        private readonly IRecentlyVisitedCompanyRepository _recentlyVisitedCompanyRepository;

        private IncomeStatementPageViewModel _incomeStatementPage;
        private BalanceSheetPageViewModel _balanceSheetPage;
        private CashFlowPageViewModel _cashFlowPage;
        private DisplayUnit _displayUnit;
        private string _currency;
        private double _closingStockPrice;
        private int _selectedIndex;
        private bool _annualData;
        private bool _quarterlyData;

        public StatementsPageViewModel(
            IWebService webService,
            IInternalValueService internalValueService,
            IRecentlyVisitedCompanyRepository recentlyVisitedCompanyRepository,
            IApplicationSettings applicationSettings,
            INavigationService navigationService)
            : base(navigationService)
        {
            _webService = webService;
            _internalValueService = internalValueService;
            _recentlyVisitedCompanyRepository = recentlyVisitedCompanyRepository;

            CanGoBack = true;
            HasTitleBar = true;
            HasBottomNavigation = false;

            AnnualData = true;
            QuarterlyData = false;

            IncomeStatementPage = new IncomeStatementPageViewModel(webService, applicationSettings, navigationService);
            BalanceSheetPage = new BalanceSheetPageViewModel(webService, applicationSettings, navigationService);
            CashFlowPage = new CashFlowPageViewModel(webService, applicationSettings, navigationService);

            LoadAnnualDataCommand = new AsyncCommand(ExecuteLoadAnnualDataCommandAsync);
            LoadQuarterlyDataCommand = new AsyncCommand(ExecuteLoadQuarterlyDataCommandAsync);
        }

        private SearchResult SearchResult { get; set; }

        private DisplayUnit DisplayUnit
        {
            get => _displayUnit;
            set
            {
                _displayUnit = value;
                RaisePropertyChanged(nameof(DisplayUnitsText));
            }
        }

        public string DisplayUnitsText => Loc.Text(TranslationKeys.AllNumbersInUnit, Loc.Text(DisplayUnit));

        public string Currency
        {
            get => _currency;
            set => SetProperty(ref _currency, value);
        }

        public double ClosingStockPrice
        {
            get => _closingStockPrice;
            set => SetProperty(ref _closingStockPrice, value);
        }

        public IncomeStatementPageViewModel IncomeStatementPage
        {
            get => _incomeStatementPage;
            set => SetProperty(ref _incomeStatementPage, value);
        }

        public BalanceSheetPageViewModel BalanceSheetPage
        {
            get => _balanceSheetPage;
            set => SetProperty(ref _balanceSheetPage, value);
        }

        public CashFlowPageViewModel CashFlowPage
        {
            get => _cashFlowPage;
            set => SetProperty(ref _cashFlowPage, value);
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (SetProperty(ref _selectedIndex, value))
                {
                    RaisePropertyChanged(nameof(StatementTitle));
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

        public string StatementTitle
        {
            get
            {
                switch (SelectedIndex)
                {
                    case 0:
                        return Loc.Text(TranslationKeys.IncomeStatement);
                    case 1:
                        return Loc.Text(TranslationKeys.BalanceSheetStatement);
                    case 2:
                        return Loc.Text(TranslationKeys.CashFlowStatement);
                    default:
                        return string.Empty;
                }
            }
        }

        public ICommand LoadAnnualDataCommand { get; }

        public ICommand LoadQuarterlyDataCommand { get; }

        public async Task TabItemChangedAsync(int index)
        {
            using (new OperationMonitor(OperationScope))
            {
                if (index == 1)
                {
                    await BalanceSheetPage.InitializeAsync(IncomeStatementPage.IncomeStatements, SearchResult, QuarterlyData, DisplayUnit);
                }
                else if (index == 2)
                {
                    await CashFlowPage.InitializeAsync(IncomeStatementPage.IncomeStatements, SearchResult, QuarterlyData, DisplayUnit);
                }
            }
        }

        protected override async Task LoadDataAsync(INavigationParameters navigationParameters)
        {
            if (navigationParameters.GetNavigationMode() == NavigationMode.Back)
                return;

            SearchResult = navigationParameters.GetValue<SearchResult>();
            Title = SearchResult.Name;
            Currency = SearchResult.Currency;

            using (new OperationMonitor(OperationScope))
            {
                DisplayUnit = await _internalValueService.GetValueAsync(InternalValues.DefaultDisplayUnit);

                var recentlyVisitedCompany = SearchResult.ToRecentlyVisitedCompany(DateTime.Now);
                await _recentlyVisitedCompanyRepository.InsertOrUpdateAsync(recentlyVisitedCompany);
                await IncomeStatementPage.InitializeAsync(SearchResult, QuarterlyData, DisplayUnit);
                await InitializeStockPriceAsync();
            }
        }

        private async Task InitializeStockPriceAsync()
        {
            var httpRequestResult = await _webService.GetHistoricalDailyPricesAsync(SearchResult.Symbol, DateTime.Now.AddDays(-10), DateTime.Now);
            if (httpRequestResult.State == HttpRequestState.Success)
            {
                var stockPrice = httpRequestResult.Payload.Historical.OrderByDescending(x => x.Date).FirstOrDefault();
                if (stockPrice != null)
                {
                    ClosingStockPrice = stockPrice.Close;
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
            return ReloadDataAsync();
        }

        private Task ExecuteLoadQuarterlyDataCommandAsync()
        {
            if (!QuarterlyData)
            {
                QuarterlyData = true;
                return Task.CompletedTask;
            }

            AnnualData = false;
            return ReloadDataAsync();
        }

        private async Task ReloadDataAsync()
        {
            using (new OperationMonitor(OperationScope))
            {
                await IncomeStatementPage.InitializeAsync(SearchResult, QuarterlyData, DisplayUnit);
                var incomeStatements = IncomeStatementPage.IncomeStatements.ToArray();
                if (incomeStatements.Any())
                {
                    BalanceSheetPage.ClearData();
                    CashFlowPage.ClearData();

                    if (SelectedIndex == 1)
                    {
                        await BalanceSheetPage.InitializeAsync(incomeStatements, SearchResult, QuarterlyData, DisplayUnit);
                    }
                    else if (SelectedIndex == 2)
                    {
                        await CashFlowPage.InitializeAsync(incomeStatements, SearchResult, QuarterlyData, DisplayUnit);
                    }
                }
            }
        }
    }
}
