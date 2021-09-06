using FinStat.Business.Configuration;
using FinStat.Common;
using FinStat.Domain.Interfaces.Configuration;
using Prism.Ioc;

namespace FinStat.Business
{
    public class BusinessModule : IUnityModule
    {
        public void RegisterServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IApplicationSettings, ApplicationSettings>();
        }
    }
}
