using System.Threading.Tasks;

namespace FinStat.DataAccess.Providers
{
    public interface IAppDbContextProvider
    {
        IAppDbContext Context { get; }

        Task CloseAsync();
    }
}
