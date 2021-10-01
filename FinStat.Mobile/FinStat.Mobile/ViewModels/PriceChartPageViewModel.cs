using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinStat.Common.Utils;
using FinStat.Domain.Enums;
using FinStat.Domain.Interfaces.Configuration;
using FinStat.Domain.Interfaces.Services;
using FinStat.Domain.Models;
using FinStat.Mobile.Commands;
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

        private IEnumerable<PriceChartPointViewModel> _chartData = new List<PriceChartPointViewModel>();
        private bool _isMin1;
        private bool _isMin5;
        private bool _isMin15;
        private bool _isMin30;
        private bool _isHour1;
        private bool _isHour4;

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

            ReloadDataCommand = new AsyncCommand<TimeFrame>(ExecuteReloadDataCommandAsync, CanExecuteReloadDataCommand);
        }

        private SearchResult SearchResult { get; set; }

        public IEnumerable<PriceChartPointViewModel> ChartData
        {
            get => _chartData;
            set
            {
                if (SetProperty(ref _chartData, value))
                {
                    RaisePropertyChanged(nameof(HasDataToPlot));
                    ReloadDataCommand.ChangeCanExecute();
                }
            }
        }

        public bool HasDataToPlot => ChartData.Any();

        public bool IsMin1
        {
            get => _isMin1;
            set => _isMin1 = value;
        }

        public bool IsMin5
        {
            get => _isMin5;
            set => _isMin5 = value;
        }

        public bool IsMin15
        {
            get => _isMin15;
            set => _isMin15 = value;
        }

        public bool IsMin30
        {
            get => _isMin30;
            set => _isMin30 = value;
        }

        public bool IsHour1
        {
            get => _isHour1;
            set => _isHour1 = value;
        }

        public bool IsHour4
        {
            get => _isHour4;
            set => _isHour4 = value;
        }

        public IAsyncCommand<TimeFrame> ReloadDataCommand { get; }

        protected override async Task LoadDataAsync(INavigationParameters navigationParameters)
        {
            SearchResult = navigationParameters.GetValue<SearchResult>();
            if (SearchResult == null)
                return;

            Title = $"{SearchResult.Name} ({SearchResult.Currency})";

            await InitializeChartDataAsync(SearchResult.Symbol, _applicationSettings.DefaultTimeFrame);
        }

        private bool CanExecuteReloadDataCommand()
        {
            return SearchResult != null;
        }

        private async Task ExecuteReloadDataCommandAsync(TimeFrame timeFrame)
        {
            using (new OperationMonitor(OperationScope))
            {
                await InitializeChartDataAsync(SearchResult.Symbol, timeFrame);
            }
        }

        private async Task InitializeChartDataAsync(string symbol, TimeFrame timeFrame)
        {
            ChangeTimeFrame(timeFrame);

            var httpRequestResult = await _webService.GetHistoricalPricesAsync(symbol, timeFrame);
            if (httpRequestResult.State == HttpRequestState.Success)
            {
                ChartData = httpRequestResult.Payload.Reverse().Select(x => new PriceChartPointViewModel
                {
                    Title = x.Date.ToString("dd.MM HH:mm"),
                    High = x.High,
                    Low = x.Low,
                    Open = x.Open,
                    Close = x.Close
                });
            }
            else
            {
                ChartData = new List<PriceChartPointViewModel>();
            }
        }

        private void ChangeTimeFrame(TimeFrame timeFrame)
        {
            IsMin1 = timeFrame == TimeFrame.Min1;
            IsMin5 = timeFrame == TimeFrame.Min5;
            IsMin15 = timeFrame == TimeFrame.Min15;
            IsMin30 = timeFrame == TimeFrame.Min30;
            IsHour1 = timeFrame == TimeFrame.Hour1;
            IsHour4 = timeFrame == TimeFrame.Hour4;
            RaiseTimeFramePropertiesChanged();
        }

        private void RaiseTimeFramePropertiesChanged()
        {
            RaisePropertyChanged(nameof(IsMin1));
            RaisePropertyChanged(nameof(IsMin5));
            RaisePropertyChanged(nameof(IsMin15));
            RaisePropertyChanged(nameof(IsMin30));
            RaisePropertyChanged(nameof(IsHour1));
            RaisePropertyChanged(nameof(IsHour4));
        }
    }
}
