using System.Threading.Tasks;
using FinStat.Common.Utils;
using FinStat.Domain.Models;
using FinStat.Mobile.Extensions;
using FinStat.Resources.Localization;
using Prism.Navigation;

namespace FinStat.Mobile.ViewModels
{
    public class PriceChartPageViewModel : ViewModelBase
    {
        public PriceChartPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
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

                await Task.CompletedTask;
            }
        }
    }
}
