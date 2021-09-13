using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FinStat.Business.Extensions;
using FinStat.Common.Utils;
using FinStat.Domain.Enums;
using FinStat.Domain.Interfaces.Configuration;
using FinStat.Domain.Interfaces.Repositories;
using FinStat.Domain.Interfaces.Services;
using FinStat.Domain.Models;
using FinStat.Mobile.Commands;
using FinStat.Mobile.Extensions;
using FinStat.Mobile.Navigation;
using FinStat.Resources.Localization;
using Prism.Navigation;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace FinStat.Mobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IWebService _webService;
        private readonly IRecentlyVisitedCompanyRepository _recentlyVisitedCompanyRepository;
        private readonly IApplicationSettings _applicationSettings;

        private int _selectedExchange;
        private string _searchQuery;
        private IEnumerable<SearchResult> _searchResults;
        private IEnumerable<SearchResult> _recentlyVisitedCompanies;

        public MainPageViewModel(
            IWebService webService,
            IRecentlyVisitedCompanyRepository recentlyVisitedCompanyRepository,
            IApplicationSettings applicationSettings,
            INavigationService navigationService)
            : base(navigationService)
        {
            _webService = webService;
            _recentlyVisitedCompanyRepository = recentlyVisitedCompanyRepository;
            _applicationSettings = applicationSettings;

            HasTitleBar = true;
            HasBottomNavigation = true;

            Title = Loc.Text(TranslationKeys.ApplicationTitle);
            SelectedExchange = 1;

            SearchCommand = new AsyncCommand<string>(ExecuteSearchCommandAsync);
            NavigateToIncomeStatementCommand = new AsyncCommand<ItemTappedEventArgs>(ExecuteNavigateToIncomeStatementCommandAsync);
        }

        private ExchangeViewModel[] Exchanges { get; } = new[]
        {
            new ExchangeViewModel(Exchange.Amex),
            new ExchangeViewModel(Exchange.Nasdaq),
            new ExchangeViewModel(Exchange.Nyse),
            new ExchangeViewModel(Exchange.Euronext),
            new ExchangeViewModel(Exchange.Lse)
        };

        public IEnumerable<string> ExchangeNames => Exchanges.Select(x => x.Text).ToList();

        public int SelectedExchange
        {
            get => _selectedExchange;
            set
            {
                if (SetProperty(ref _selectedExchange, value))
                {
                    AsyncHelper.RunSync(() => SearchCompanyAsync(SearchQuery));
                }
            }
        }

        private string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (SetProperty(ref _searchQuery, value))
                {
                    RaisePropertyChanged(nameof(IsSearchingActive));
                }
            }
        }

        public bool IsSearchingActive => !string.IsNullOrWhiteSpace(SearchQuery);

        public IEnumerable<SearchResult> SearchResults
        {
            get => _searchResults;
            set
            {
                if (SetProperty(ref _searchResults, value))
                {
                    RaisePropertyChanged(nameof(AnySearchResults));
                }
            }
        }

        public bool AnySearchResults => SearchResults != null && SearchResults.Any();

        public IEnumerable<SearchResult> RecentlyVisitedCompanies
        {
            get => _recentlyVisitedCompanies;
            set
            {
                if (SetProperty(ref _recentlyVisitedCompanies, value))
                {
                    RaisePropertyChanged(nameof(AnyRecentlyVisitedCompanies));
                }
            }
        }

        public bool AnyRecentlyVisitedCompanies => RecentlyVisitedCompanies != null && RecentlyVisitedCompanies.Any();

        public ICommand SearchCommand { get; }

        public ICommand NavigateToIncomeStatementCommand { get; }

        protected override async Task LoadDataAsync(INavigationParameters navigationParameters)
        {
            using (new OperationMonitor(OperationScope))
            {
                var recentlyVisitedCompanies = await _recentlyVisitedCompanyRepository.GetLastRecordsAsync(_applicationSettings.SearchLimit);
                RecentlyVisitedCompanies = recentlyVisitedCompanies.Select(x => x.ToSearchResult());
            }
        }

        private Task ExecuteSearchCommandAsync(string query)
        {
            SearchQuery = query;

            return SearchCompanyAsync(query);
        }

        private async Task SearchCompanyAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return;

            using (new OperationMonitor(OperationScope))
            {
                var exchange = Exchanges[SelectedExchange];
                var result = await HandleWebCallAsync(() => _webService.SearchCompanyAsync(query, exchange.Value, _applicationSettings.SearchLimit));
                if (!result.success)
                    return;

                SearchResults = result.payload;
            }
        }

        private Task ExecuteNavigateToIncomeStatementCommandAsync(ItemTappedEventArgs args)
        {
            var searchResult = args.ItemData as SearchResult;
            if (searchResult == null)
                return DisplayAlertAsync(string.Empty, Loc.Text(TranslationKeys.GeneralErrorMessage));

            var navigationParameters = new NavigationParameters();
            navigationParameters.Add<SearchResult>(searchResult);
            return NavigationService.NavigateWithoutAnimationAsync(Pages.Statements, navigationParameters);
        }
    }
}
