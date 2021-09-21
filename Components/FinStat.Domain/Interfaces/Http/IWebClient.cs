using System.Threading;
using System.Threading.Tasks;
using FinStat.Domain.Enums;
using FinStat.Domain.Models;

namespace FinStat.Domain.Interfaces.Http
{
    public interface IWebClient
    {
        Task<SearchResult[]> SearchCompanyAsync(string query, Exchange exchange, int limit, CancellationToken cancellationToken);

        Task<IncomeStatement[]> GetIncomeStatementsAsync(string ticker, bool isQuarterPeriod, int limit, CancellationToken cancellationToken);

        Task<BalanceSheet[]> GetBalanceSheetStatementsAsync(string ticker, bool isQuarterPeriod, int limit, CancellationToken cancellationToken);

        Task<CashFlow[]> GetCashFlowStatementsAsync(string ticker, bool isQuarterPeriod, int limit, CancellationToken cancellationToken);
    }
}
