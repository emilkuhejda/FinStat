using System.Threading.Tasks;
using System.Windows.Input;
using FinStat.Domain.Enums;
using FinStat.Mobile.Commands;
using FinStat.Mobile.Extensions;
using FinStat.Mobile.Navigation;
using FinStat.Mobile.Utils;
using FinStat.Mobile.Views;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace FinStat.Mobile.ViewModels
{
    public class BottomNavigationViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        public BottomNavigationViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NavigateToMainPageCommand = new AsyncCommand(ExecuteNavigateToMainPageCommandAsync, CanExecuteNavigateToMainPageCommand);
            NavigateToMorePageCommand = new AsyncCommand(ExecuteNavigateToMorePageCommandAsync, CanExecuteNavigateToMorePageCommand);
        }

        public bool IsMainPageActive => Navigator.CurrentPage == RootPage.Main;

        public bool IsMorePageActive => Navigator.CurrentPage == RootPage.More;

        public ICommand NavigateToMainPageCommand { get; }

        public ICommand NavigateToMorePageCommand { get; }

        private bool CanExecuteNavigateToMainPageCommand()
        {
            return Navigator.CurrentPage != RootPage.Main;
        }

        private Task ExecuteNavigateToMainPageCommandAsync()
        {
            return NavigateToRootAsync($"/{Pages.Navigation}/{Pages.Main}", RootPage.Main);
        }

        private bool CanExecuteNavigateToMorePageCommand()
        {
            return Navigator.CurrentPage != RootPage.More;
        }

        private Task ExecuteNavigateToMorePageCommandAsync()
        {
            return NavigateToRootAsync($"/{Pages.Navigation}/{Pages.More}", RootPage.More);
        }

        public Task NavigateToRootAsync(string name, INavigationParameters navigationParameters = null)
        {
            DisposePages();

            Navigator.ResetNavigation();

            return _navigationService.NavigateWithoutAnimationAsync(name, navigationParameters);
        }

        public async Task NavigateToRootAsync(string name, RootPage rootPage, INavigationParameters navigationParameters = null)
        {
            DisposePages();

            Navigator.CurrentPage = rootPage;

            await _navigationService.NavigateWithoutAnimationAsync(name, navigationParameters).ConfigureAwait(false);

            RaisePropertiesChanged();
        }

        private void DisposePages()
        {
            var navigationPage = Application.Current.MainPage as FinStatNavigationPage;
            navigationPage?.Pages.ForEach(x => ((ViewModelBase)x.BindingContext).Dispose());
        }

        private void RaisePropertiesChanged()
        {
            RaisePropertyChanged(nameof(IsMainPageActive));
            RaisePropertyChanged(nameof(IsMorePageActive));
        }
    }
}
