using FinStat.Domain.Interfaces.Required;
using FinStat.Mobile.Configuration;
using FinStat.Mobile.Droid.Localization;
using FinStat.Mobile.Droid.Providers;
using Prism.Ioc;

namespace FinStat.Mobile.Droid.Configuration
{
    public class AndroidBootstrapper : Bootstrapper
    {
        protected override void RegisterPlatformServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IApplicationVersionProvider, ApplicationVersionProvider>();
            containerRegistry.RegisterSingleton<ILocalizer, Localizer>();
            containerRegistry.RegisterSingleton<IDirectoryProvider, DirectoryProvider>();
        }
    }
}