using System.Threading.Tasks;
using FinStat.Common.Utils;
using FinStat.Mobile.Extensions;
using FinStat.Mobile.ViewModels.DataGrid;
using Prism.Navigation;

namespace FinStat.Mobile.ViewModels
{
    public class ChartPageViewModel : ViewModelBase
    {
        public ChartPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            CanGoBack = true;
            HasTitleBar = true;
        }

        protected override async Task LoadDataAsync(INavigationParameters navigationParameters)
        {
            using (new OperationMonitor(OperationScope))
            {
                var rowViewModel = navigationParameters.GetValue<RowViewModel>();
                await Task.CompletedTask;
            }
        }
    }
}
