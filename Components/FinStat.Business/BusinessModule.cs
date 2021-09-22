using FinStat.Business.Configuration;
using FinStat.Business.Services;
using FinStat.Business.Utils;
using FinStat.Common;
using FinStat.Domain.Interfaces.Configuration;
using FinStat.Domain.Interfaces.Services;
using FinStat.Domain.Interfaces.Utils;
using Prism.Ioc;

namespace FinStat.Business
{
    public class BusinessModule : IUnityModule
    {
        public void RegisterServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IInternalValueService, InternalValueService>();
            containerRegistry.RegisterSingleton<IConnectivityService, ConnectivityService>();
            containerRegistry.RegisterSingleton<IWebService, WebService>();

            containerRegistry.RegisterSingleton<IWebServiceErrorHandler, WebServiceErrorHandler>();
            containerRegistry.RegisterSingleton<IApplicationSettings, ApplicationSettings>();
        }
    }
}
