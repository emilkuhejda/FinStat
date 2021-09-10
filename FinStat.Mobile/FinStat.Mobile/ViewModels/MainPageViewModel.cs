using System.Threading.Tasks;
using FinStat.Common.Utils;
using Prism.Navigation;

namespace FinStat.Mobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            HasBottomNavigation = true;
        }

        protected override async Task LoadDataAsync(INavigationParameters navigationParameters)
        {
            using (new OperationMonitor(OperationScope))
            {
                await Task.CompletedTask;
            }
        }
    }
}
