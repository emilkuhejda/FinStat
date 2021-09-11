using System.Threading.Tasks;
using FinStat.Common.Utils;
using FinStat.Domain.Interfaces.Services;
using FinStat.Domain.Models;
using FinStat.Mobile.Extensions;
using Prism.Navigation;

namespace FinStat.Mobile.ViewModels
{
    public class IncomeStatementPageViewModel : ViewModelBase
    {
        private readonly IWebService _webService;

        public IncomeStatementPageViewModel(
            IWebService webService,
            INavigationService navigationService)
            : base(navigationService)
        {
            _webService = webService;

            CanGoBack = true;
            HasTitleBar = true;
            HasBottomNavigation = true;
        }

        protected override async Task LoadDataAsync(INavigationParameters navigationParameters)
        {
            using (new OperationMonitor(OperationScope))
            {
                var searchResult = navigationParameters.GetValue<SearchResult>();
                Title = searchResult.Name;
            }
        }
    }
}
