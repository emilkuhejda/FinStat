using Prism.Mvvm;
using Prism.Navigation;

namespace FinStat.Mobile.ViewModels
{
    public class BottomNavigationViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        public BottomNavigationViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
