using FinStat.Mobile.Extensions;
using FinStat.Mobile.Navigation;
using Prism;
using Prism.Ioc;
using Prism.Unity;

namespace FinStat.Mobile
{
    public partial class App : PrismApplication
    {

        public App(IPlatformInitializer platformInitializer)
            : base(platformInitializer)
        {
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateWithoutAnimationAsync($"/{Pages.Main}").ConfigureAwait(false);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
