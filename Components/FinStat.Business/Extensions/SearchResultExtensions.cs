using System;
using FinStat.Domain.Models;

namespace FinStat.Business.Extensions
{
    public static class SearchResultExtensions
    {
        public static RecentlyVisitedCompany ToRecentlyVisitedCompany(this SearchResult searchResult, DateTime dateVisited)
        {
            return new RecentlyVisitedCompany
            {
                Symbol = searchResult.Symbol,
                Name = searchResult.Symbol,
                Currency = searchResult.Currency,
                StockExchange = searchResult.StockExchange,
                ExchangeShortName = searchResult.ExchangeShortName,
                LastVisited = dateVisited
            };
        }
    }
}
