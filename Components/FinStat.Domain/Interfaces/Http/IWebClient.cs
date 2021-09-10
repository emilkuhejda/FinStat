using System.Threading;
using System.Threading.Tasks;
using FinStat.Domain.Models;

namespace FinStat.Domain.Interfaces.Http
{
    public interface IWebClient
    {
        Task<IncomeStatement[]> GetIncomeStatementsAsync(string ticker, bool isQuarterPeriod, int limit, CancellationToken cancellationToken);
    }
}
