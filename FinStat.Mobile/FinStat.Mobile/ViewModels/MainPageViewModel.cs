using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FinStat.Common.Utils;
using FinStat.Domain.Enums;
using FinStat.Domain.Interfaces.Services;
using FinStat.Domain.Models;
using FinStat.Mobile.Commands;
using FinStat.Mobile.Extensions;
using FinStat.Mobile.Navigation;
using FinStat.Resources.Localization;
using Prism.Navigation;

namespace FinStat.Mobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private const int SearchLimit = 20;

        private readonly IWebService _webService;

        private string _searchQuery;
        private int _selectedExchange;
        private SearchResult _selectedSearchResult;
        private IEnumerable<SearchResult> _searchResults;

        public MainPageViewModel(
            IWebService webService,
            INavigationService navigationService)
            : base(navigationService)
        {
            _webService = webService;

            HasTitleBar = true;
            HasBottomNavigation = true;

            Title = Loc.Text(TranslationKeys.ApplicationTitle);
            SelectedExchange = 1;

            SearchCommand = new AsyncCommand<string>(ExecuteSearchCommandAsync);
            NavigateToIncomeStatementCommand = new AsyncCommand(ExecuteNavigateToIncomeStatementCommandAsync);
        }

        private ExchangeViewModel[] Exchanges { get; } = new[]
        {
            new ExchangeViewModel(Exchange.Amex),
            new ExchangeViewModel(Exchange.Nasdaq),
            new ExchangeViewModel(Exchange.Nyse),
            new ExchangeViewModel(Exchange.Euronext),
            new ExchangeViewModel(Exchange.Lse)
        };

        public int SelectedExchange
        {
            get => _selectedExchange;
            set
            {
                if (SetProperty(ref _selectedExchange, value))
                {
                    AsyncHelper.RunSync(() => SearchCompanyAsync(_searchQuery));
                }
            }
        }

        public SearchResult SelectedSearchResult
        {
            get => _selectedSearchResult;
            set => SetProperty(ref _selectedSearchResult, value);
        }

        public IEnumerable<SearchResult> SearchResults
        {
            get => _searchResults;
            set => SetProperty(ref _searchResults, value);
        }

        public IEnumerable<string> ExchangeNames => Exchanges.Select(x => x.Text).ToList();

        public ICommand SearchCommand { get; }

        public ICommand NavigateToIncomeStatementCommand { get; }

        protected override async Task LoadDataAsync(INavigationParameters navigationParameters)
        {
            using (new OperationMonitor(OperationScope))
            {
                await Task.CompletedTask;
            }
        }

        private Task ExecuteSearchCommandAsync(string query)
        {
            _searchQuery = query;

            return SearchCompanyAsync(query);
        }

        private async Task SearchCompanyAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return;

            using (new OperationMonitor(OperationScope))
            {
                var exchange = Exchanges[SelectedExchange];
                var result = await HandleWebCallAsync(() => _webService.SearchCompanyAsync(query, exchange.Value, SearchLimit));
                if (!result.success)
                    return;

                SearchResults = result.payload;
            }
        }

        private Task ExecuteNavigateToIncomeStatementCommandAsync()
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add<SearchResult>(SelectedSearchResult);
            return NavigationService.NavigateWithoutAnimationAsync(Pages.IncomeStatement, navigationParameters);
        }
    }
}
