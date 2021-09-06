using FinStat.Domain.Interfaces.Required;
using FinStat.Mobile.Localization;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Unity;

namespace FinStat.Mobile.Configuration
{
    public abstract class Bootstrapper : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var mobileModule = new MobileModule();
            mobileModule.RegisterServices(containerRegistry);

            RegisterPlatformServices(containerRegistry);
            InitializeServices(containerRegistry);
        }

        protected abstract void RegisterPlatformServices(IContainerRegistry containerRegistry);

        private void InitializeServices(IContainerRegistry containerRegistry)
        {
            LocalizationExtension.Init(() => containerRegistry.GetContainer().Resolve<ILocalizer>());
        }
    }
}
