using System;
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

        Task<StockPrice[]> GetStockPriceAsync(string ticker, CancellationToken cancellationToken);

        Task<HistoricalDailyPrice> GetHistoricalDailyPricesAsync(string ticker, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken);

        Task<HistoricalPrice[]> GetHistoricalPricesAsync(string ticker, TimeFrame timeFrame, CancellationToken cancellationToken);
    }
}
