using FinStat.Common;
using FinStat.Mobile.Navigation;
using FinStat.Mobile.ViewModels;
using FinStat.Mobile.Views;
using Prism.Ioc;

namespace FinStat.Mobile.Configuration
{
    public class MobileModule : IUnityModule
    {
        public void RegisterServices(IContainerRegistry containerRegistry)
        {
            RegisterPages(containerRegistry);
        }

        private static void RegisterPages(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<FinStatNavigationPage>(Pages.Navigation);
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>(Pages.Main);
            containerRegistry.RegisterForNavigation<MorePage, MorePageViewModel>(Pages.More);
            containerRegistry.RegisterForNavigation<StatementsPage, StatementsPageViewModel>(Pages.Statements);
        }
    }
}
