using FinStat.Common;
using FinStat.Domain.Interfaces.Services;
using FinStat.Mobile.Navigation;
using FinStat.Mobile.Services;
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

            containerRegistry.RegisterSingleton<IEmailService, EmailService>();
        }

        private static void RegisterPages(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<FinStatNavigationPage>(Pages.Navigation);
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>(Pages.Main);
            containerRegistry.RegisterForNavigation<MorePage, MorePageViewModel>(Pages.More);
            containerRegistry.RegisterForNavigation<StatementsPage, StatementsPageViewModel>(Pages.Statements);
            containerRegistry.RegisterForNavigation<ChartPage, ChartPageViewModel>(Pages.Chart);
            containerRegistry.RegisterForNavigation<DropDownListPage, DropDownListPageViewModel>(Pages.DropDownListPage);
        }
    }
}
