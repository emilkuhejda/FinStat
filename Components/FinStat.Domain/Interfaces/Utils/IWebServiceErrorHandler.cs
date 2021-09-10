using System;
using System.Threading.Tasks;
using FinStat.Domain.Http;
using FinStat.Domain.Interfaces.Http;

namespace FinStat.Domain.Interfaces.Utils
{
    public interface IWebServiceErrorHandler
    {
        Task<HttpRequestResult<T>> HandleResponseAsync<T>(Func<IWebClient, Task<T>> webServiceCall) where T : class;
    }
}
