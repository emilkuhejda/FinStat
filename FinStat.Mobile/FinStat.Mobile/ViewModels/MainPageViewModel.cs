using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FinStat.Common.Utils;
using FinStat.Domain.Enums;
using FinStat.Domain.Interfaces.Services;
using FinStat.Mobile.Commands;
using FinStat.Resources.Localization;
using Prism.Navigation;

namespace FinStat.Mobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private const int SearchLimit = 10;

        private readonly IWebService _webService;

        private int _selectedExchange;

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
            set => SetProperty(ref _selectedExchange, value);
        }

        public IEnumerable<string> ExchangeNames => Exchanges.Select(x => x.Text).ToList();

        public ICommand SearchCommand { get; }

        protected override async Task LoadDataAsync(INavigationParameters navigationParameters)
        {
            using (new OperationMonitor(OperationScope))
            {
                await Task.CompletedTask;
            }
        }

        private async Task ExecuteSearchCommandAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return;

            var exchange = Exchanges[SelectedExchange];
            var result = await HandleWebCallAsync(() => _webService.SearchCompanyAsync(query, exchange.Value, SearchLimit));
            if (!result.success)
                return;
        }
    }
}
