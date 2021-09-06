using FinStat.Domain.Interfaces.Configuration;
using FinStat.Mobile.Extensions;
using FinStat.Mobile.Navigation;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Syncfusion.Licensing;

namespace FinStat.Mobile
{
    public partial class App : PrismApplication
    {
        private IApplicationSettings _applicationSettings;

        public App(IPlatformInitializer platformInitializer)
            : base(platformInitializer)
        {
        }

        protected override void OnInitialized()
        {
            _applicationSettings = Container.Resolve<IApplicationSettings>();

            SyncfusionLicenseProvider.RegisterLicense(_applicationSettings.SyncfusionKey);

            InitializeComponent();

            NavigationService.NavigateWithoutAnimationAsync($"/{Pages.Main}").ConfigureAwait(false);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
