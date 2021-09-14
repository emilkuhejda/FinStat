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

            LoadAnnualDataCommand = new AsyncCommand(ExecuteLoadAnnualDataCommandAsync);
            LoadQuarterlyDataCommand = new AsyncCommand(ExecuteLoadQuarterlyDataCommandAsync);
        }

        private IncomeStatement[] IncomeStatements { get; set; } = Enumerable.Empty<IncomeStatement>().ToArray();

        private SearchResult SearchResult { get; set; }

        private IncomeStatementPageViewModel IncomeStatementPage { get; }

        private BalanceSheetPageViewModel BalanceSheetPage { get; }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set => SetProperty(ref _selectedIndex, value);
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

        public async Task TabItemChangedAsync(int index)
        {
            using (new OperationMonitor(OperationScope))
            {
                if (index == 1)
                {
                    await BalanceSheetPage.InitializeAsync(IncomeStatements, SearchResult, QuarterlyData);
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
                IncomeStatements = await IncomeStatementPage.InitializeAsync(SearchResult, QuarterlyData);
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
                IncomeStatements = await IncomeStatementPage.InitializeAsync(SearchResult, QuarterlyData);
            }
        }
    }
}
