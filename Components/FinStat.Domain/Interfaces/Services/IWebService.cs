using System;
using System.Threading;
using System.Threading.Tasks;
using FinStat.Domain.Enums;
using FinStat.Domain.Http;
using FinStat.Domain.Models;

namespace FinStat.Domain.Interfaces.Services
{
    public interface IWebService
    {
        Task<HttpRequestResult<SearchResult[]>> SearchCompanyAsync(string query, Exchange exchange, int limit, CancellationToken cancellationToken = default);

        Task<HttpRequestResult<IncomeStatement[]>> GetIncomeStatementsAsync(string ticker, bool isQuarterPeriod, int limit, CancellationToken cancellationToken = default);

        Task<HttpRequestResult<BalanceSheet[]>> GetBalanceSheetStatementsAsync(string ticker, bool isQuarterPeriod, int limit, CancellationToken cancellationToken = default);

        Task<HttpRequestResult<CashFlow[]>> GetCashFlowStatementsAsync(string ticker, bool isQuarterPeriod, int limit, CancellationToken cancellationToken = default);

        Task<HttpRequestResult<StockPrice[]>> GetStockPriceAsync(string ticker, CancellationToken cancellationToken = default);

        Task<HttpRequestResult<HistoricalDailyPrice>> GetHistoricalDailyPricesAsync(string ticker, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default);

        Task<HttpRequestResult<HistoricalPrice[]>> GetHistoricalPricesAsync(string ticker, TimeFrame timeFrame, CancellationToken cancellationToken = default);
    }
}
