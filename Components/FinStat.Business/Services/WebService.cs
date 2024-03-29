﻿using System;
using System.Threading;
using System.Threading.Tasks;
using FinStat.Domain.Enums;
using FinStat.Domain.Http;
using FinStat.Domain.Interfaces.Services;
using FinStat.Domain.Interfaces.Utils;
using FinStat.Domain.Models;

namespace FinStat.Business.Services
{
    public class WebService : IWebService
    {
        private readonly IWebServiceErrorHandler _webServiceErrorHandler;

        public WebService(IWebServiceErrorHandler webServiceErrorHandler)
        {
            _webServiceErrorHandler = webServiceErrorHandler;
        }

        public Task<HttpRequestResult<SearchResult[]>> SearchCompanyAsync(string query, Exchange exchange, int limit, CancellationToken cancellationToken = default)
        {
            return _webServiceErrorHandler.HandleResponseAsync(client => client.SearchCompanyAsync(query, exchange, limit, cancellationToken));
        }

        public Task<HttpRequestResult<IncomeStatement[]>> GetIncomeStatementsAsync(string ticker, bool isQuarterPeriod, int limit, CancellationToken cancellationToken = default)
        {
            return _webServiceErrorHandler.HandleResponseAsync(client => client.GetIncomeStatementsAsync(ticker, isQuarterPeriod, limit, cancellationToken));
        }

        public Task<HttpRequestResult<BalanceSheet[]>> GetBalanceSheetStatementsAsync(string ticker, bool isQuarterPeriod, int limit, CancellationToken cancellationToken = default)
        {
            return _webServiceErrorHandler.HandleResponseAsync(client => client.GetBalanceSheetStatementsAsync(ticker, isQuarterPeriod, limit, cancellationToken));
        }

        public Task<HttpRequestResult<CashFlow[]>> GetCashFlowStatementsAsync(string ticker, bool isQuarterPeriod, int limit, CancellationToken cancellationToken = default)
        {
            return _webServiceErrorHandler.HandleResponseAsync(client => client.GetCashFlowStatementsAsync(ticker, isQuarterPeriod, limit, cancellationToken));
        }

        public Task<HttpRequestResult<StockPrice[]>> GetStockPriceAsync(string ticker, CancellationToken cancellationToken = default)
        {
            return _webServiceErrorHandler.HandleResponseAsync(client => client.GetStockPriceAsync(ticker, cancellationToken));
        }

        public Task<HttpRequestResult<HistoricalDailyPrice>> GetHistoricalDailyPricesAsync(string ticker, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
        {
            return _webServiceErrorHandler.HandleResponseAsync(client => client.GetHistoricalDailyPricesAsync(ticker, fromDate, toDate, cancellationToken));
        }

        public Task<HttpRequestResult<HistoricalPrice[]>> GetHistoricalPricesAsync(string ticker, TimeFrame timeFrame, CancellationToken cancellationToken = default)
        {
            return _webServiceErrorHandler.HandleResponseAsync(client => client.GetHistoricalPricesAsync(ticker, timeFrame, cancellationToken));
        }
    }
}
