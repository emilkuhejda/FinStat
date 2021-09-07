using System.Threading.Tasks;

namespace FinStat.Domain.Interfaces.Repositories
{
    public interface IInternalValueRepository
    {
        Task<string> GetValue(string key);

        Task UpdateValue(string key, string value);
    }
}
