using System.Threading.Tasks;
using FinStat.Domain.Configuration;

namespace FinStat.Domain.Interfaces.Services
{
    public interface IInternalValueService
    {
        Task<T> GetValueAsync<T>(InternalValue<T> internalValue);

        Task UpdateValueAsync<T>(InternalValue<T> internalValue, T value);
    }
}
