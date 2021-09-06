using FinStat.Domain.Interfaces.Required;
using FinStat.Mobile.Configuration;
using FinStat.Mobile.UWP.Localization;
using Prism.Ioc;

namespace FinStat.Mobile.UWP.Configuration
{
    public class UwpBootstrapper : Bootstrapper
    {
        protected override void RegisterPlatformServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ILocalizer, Localizer>();
        }
    }
}
