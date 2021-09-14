using FinStat.Domain.Models;

namespace FinStat.Business.Extensions
{
    public static class RecentlyVisitedCompanyExtensions
    {
        public static SearchResult ToSearchResult(this RecentlyVisitedCompany recentlyVisitedCompany)
        {
            return new SearchResult
            {
                Symbol = recentlyVisitedCompany.Symbol,
                Name = recentlyVisitedCompany.Symbol,
                Currency = recentlyVisitedCompany.Currency,
                StockExchange = recentlyVisitedCompany.StockExchange,
                ExchangeShortName = recentlyVisitedCompany.ExchangeShortName
            };
        }
    }
}
