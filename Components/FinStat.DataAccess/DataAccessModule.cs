using FinStat.Common;
using FinStat.DataAccess.Providers;
using FinStat.DataAccess.Repositories;
using FinStat.Domain.Interfaces.Repositories;
using Prism.Ioc;

namespace FinStat.DataAccess
{
    public class DataAccessModule : IUnityModule
    {
        public void RegisterServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppDbContext, AppDbContext>();
            containerRegistry.RegisterSingleton<IAppDbContextProvider, AppDbContextProvider>();
            containerRegistry.RegisterSingleton<IStorageInitializer, StorageInitializer>();
            containerRegistry.RegisterSingleton<IInternalValueRepository, InternalValueRepository>();
        }
    }
}
