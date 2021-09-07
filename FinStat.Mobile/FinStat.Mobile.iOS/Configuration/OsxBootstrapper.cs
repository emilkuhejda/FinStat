using FinStat.Domain.Interfaces.Required;
using FinStat.Mobile.Configuration;
using FinStat.Mobile.iOS.Localization;
using FinStat.Mobile.iOS.Providers;
using Prism.Ioc;

namespace FinStat.Mobile.iOS.Configuration
{
    public class OsxBootstrapper : Bootstrapper
    {
        protected override void RegisterPlatformServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ILocalizer, Localizer>();
            containerRegistry.RegisterSingleton<IDirectoryProvider, DirectoryProvider>();
        }
    }
}