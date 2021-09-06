using FinStat.Domain.Interfaces.Required;
using FinStat.Mobile.Configuration;
using FinStat.Mobile.Droid.Localization;
using Prism.Ioc;

namespace FinStat.Mobile.Droid.Configuration
{
    public class AndroidBootstrapper : Bootstrapper
    {
        protected override void RegisterPlatformServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ILocalizer, Localizer>();
        }
    }
}