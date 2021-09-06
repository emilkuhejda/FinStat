using Prism.Ioc;

namespace FinStat.Common
{
    public interface IUnityModule
    {
        void RegisterServices(IContainerRegistry containerRegistry);
    }
}
