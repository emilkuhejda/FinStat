using Prism.Navigation;

namespace FinStat.Mobile.ViewModels
{
    public class MorePageViewModel : ViewModelBase
    {
        public MorePageViewModel(
            INavigationService navigationService)
            : base(navigationService)
        {
            HasBottomNavigation = true;
        }
    }
}
