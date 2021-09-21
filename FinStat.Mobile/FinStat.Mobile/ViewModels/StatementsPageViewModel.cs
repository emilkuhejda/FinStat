using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FinStat.Business.Extensions;
using FinStat.Common.Utils;
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
        private readonly IRecentlyVisitedCompanyRepository _recentlyVisitedCompanyRepository;

        private IncomeStatementPageViewModel _incomeStatementPage;
        private BalanceSheetPageViewModel _balanceSheetPage;
        private CashFlowPageViewModel _cashFlowPage;
        private int _selectedIndex;
        private bool _annualData;
        private bool _quarterlyData;
        private string _displayUnitsText;

        public StatementsPageViewModel(
            IRecentlyVisitedCompanyRepository recentlyVisitedCompanyRepository,
            IWebService webService,
            IApplicationSettings applicationSettings,
            INavigationService navigationService)
            : base(navigationService)
        {
            _recentlyVisitedCompanyRepository = recentlyVisitedCompanyRepository;

            CanGoBack = true;
            HasTitleBar = true;
            HasBottomNavigation = false;

            AnnualData = true;
            QuarterlyData = false;

            DisplayUnitsText = Loc.Text(TranslationKeys.AllNumbersInUnit, Loc.Text(applicationSettings.DisplayUnit));

            IncomeStatementPage = new IncomeStatementPageViewModel(webService, applicationSettings, navigationService);
            BalanceSheetPage = new BalanceSheetPageViewModel(webService, applicationSettings, navigationService);
            CashFlowPage = new CashFlowPageViewModel(webService, applicationSettings, navigationService);

            LoadAnnualDataCommand = new AsyncCommand(ExecuteLoadAnnualDataCommandAsync);
            LoadQuarterlyDataCommand = new AsyncCommand(ExecuteLoadQuarterlyDataCommandAsync);
        }

        private SearchResult SearchResult { get; set; }

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

        public string DisplayUnitsText
        {
            get => _displayUnitsText;
            set => SetProperty(ref _displayUnitsText, value);
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
                    await BalanceSheetPage.InitializeAsync(IncomeStatementPage.IncomeStatements, SearchResult, QuarterlyData);
                }
                else if (index == 2)
                {
                    await CashFlowPage.InitializeAsync(IncomeStatementPage.IncomeStatements, SearchResult, QuarterlyData);
                }
            }
        }

        protected override async Task LoadDataAsync(INavigationParameters navigationParameters)
        {
            SearchResult = navigationParameters.GetValue<SearchResult>();
            Title = SearchResult.Name;

            using (new OperationMonitor(OperationScope))
            {
                var recentlyVisitedCompany = SearchResult.ToRecentlyVisitedCompany(DateTime.Now);
                await _recentlyVisitedCompanyRepository.InsertOrUpdateAsync(recentlyVisitedCompany);
                await IncomeStatementPage.InitializeAsync(SearchResult, QuarterlyData);
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
                await IncomeStatementPage.InitializeAsync(SearchResult, QuarterlyData);
                var incomeStatements = IncomeStatementPage.IncomeStatements.ToArray();
                if (incomeStatements.Any())
                {
                    BalanceSheetPage.ClearData();
                    CashFlowPage.ClearData();

                    if (SelectedIndex == 1)
                    {
                        await BalanceSheetPage.InitializeAsync(incomeStatements, SearchResult, QuarterlyData);
                    }
                    else if (SelectedIndex == 2)
                    {
                        await CashFlowPage.InitializeAsync(incomeStatements, SearchResult, QuarterlyData);
                    }
                }
            }
        }
    }
}
