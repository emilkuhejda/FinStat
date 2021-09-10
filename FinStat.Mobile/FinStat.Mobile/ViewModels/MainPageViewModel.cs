using System.Threading.Tasks;
using FinStat.Common.Utils;
using FinStat.Domain.Interfaces.Services;
using Prism.Navigation;

namespace FinStat.Mobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IWebService _webService;

        public MainPageViewModel(INavigationService navigationService, IWebService webService)
            : base(navigationService)
        {
            _webService = webService;
            HasBottomNavigation = true;
        }

        protected override async Task LoadDataAsync(INavigationParameters navigationParameters)
        {
            using (new OperationMonitor(OperationScope))
            {
                var result = await _webService.GetIncomeStatementsAsync("AAPL", false, 120).ConfigureAwait(false);
            }
        }
    }
}
