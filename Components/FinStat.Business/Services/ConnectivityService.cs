using FinStat.Domain.Interfaces.Services;
using Xamarin.Essentials;

namespace FinStat.Business.Services
{
    public class ConnectivityService : IConnectivityService
    {
        public bool IsConnected { get; } = Connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}
