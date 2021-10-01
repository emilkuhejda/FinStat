using System.Threading.Tasks;
using FinStat.Common.Utils;
using FinStat.Domain.Interfaces.Configuration;
using FinStat.Domain.Interfaces.Services;
using FinStat.Domain.Models;
using FinStat.Mobile.Extensions;
using FinStat.Resources.Localization;
using Prism.Navigation;

namespace FinStat.Mobile.ViewModels
{
    public class PriceChartPageViewModel : ViewModelBase
    {
        private readonly IWebService _webService;
        private readonly IApplicationSettings _applicationSettings;

        public PriceChartPageViewModel(
            IWebService webService,
            IApplicationSettings applicationSettings,
            INavigationService navigationService)
            : base(navigationService)
        {
            _webService = webService;
            _applicationSettings = applicationSettings;

            CanGoBack = true;
            HasTitleBar = true;
            Title = Loc.Text(TranslationKeys.NoAvailableData);
        }

        public bool HasDataToPlot => false;

        protected override async Task LoadDataAsync(INavigationParameters navigationParameters)
        {
            using (new OperationMonitor(OperationScope))
            {
                var searchResult = navigationParameters.GetValue<SearchResult>();
                if (searchResult == null)
                    return;

                Title = searchResult.Symbol;

                var httpRequestResult = await _webService.GetHistoricalPricesAsync(searchResult.Symbol, _applicationSettings.DefaultTimeFrame);
            }
        }
    }
}
