using System.Threading.Tasks;

namespace FinStat.DataAccess
{
    public interface IStorageInitializer
    {
        Task InitializeAsync();
    }
}
