using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinStat.Common.Utils;
using FinStat.Domain.Enums;
using FinStat.Domain.Interfaces.Configuration;
using FinStat.Domain.Interfaces.Services;
using FinStat.Domain.Models;
using FinStat.Mobile.Extensions;
using FinStat.Mobile.ViewModels.Charts;
using FinStat.Resources.Localization;
using Prism.Navigation;

namespace FinStat.Mobile.ViewModels
{
    public class PriceChartPageViewModel : ViewModelBase
    {
        private readonly IWebService _webService;
        private readonly IApplicationSettings _applicationSettings;

        private IEnumerable<PriceChartViewModel> _chartData = new List<PriceChartViewModel>();

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

        public IEnumerable<PriceChartViewModel> ChartData
        {
            get => _chartData;
            set
            {
                if (SetProperty(ref _chartData, value))
                {
                    RaisePropertyChanged(nameof(HasDataToPlot));
                }
            }
        }

        public bool HasDataToPlot => ChartData.Any();

        protected override async Task LoadDataAsync(INavigationParameters navigationParameters)
        {
            using (new OperationMonitor(OperationScope))
            {
                var searchResult = navigationParameters.GetValue<SearchResult>();
                if (searchResult == null)
                    return;

                Title = searchResult.Name;

                var httpRequestResult = await _webService.GetHistoricalPricesAsync(searchResult.Symbol, _applicationSettings.DefaultTimeFrame);
                if (httpRequestResult.State == HttpRequestState.Success)
                {
                    ChartData = httpRequestResult.Payload.Reverse().Select(x => new PriceChartViewModel
                    {
                        Title = x.Date.ToString("dd/MM HH:mm"),
                        High = x.High,
                        Low = x.Low,
                        Open = x.Open,
                        Close = x.Close
                    });
                }
            }
        }
    }
}
