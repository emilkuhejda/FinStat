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

        public MainPageViewModel(
            IWebService webService,
            INavigationService navigationService)
            : base(navigationService)
        {
            _webService = webService;

            HasTitleBar = true;
            HasBottomNavigation = true;

            Title = Loc.Text(TranslationKeys.ApplicationTitle);

            SearchCommand = new AsyncCommand<string>(ExecuteSearchCommandAsync);
        }

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

            var result = await HandleWebCallAsync(() => _webService.SearchCompanyAsync(query, Exchange.Nasdaq, SearchLimit));
            if (!result.success)
                return;
        }
    }
}
