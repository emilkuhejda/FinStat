using FinStat.DataAccess.Entities;
using FinStat.Domain.Models;

namespace FinStat.DataAccess.DataAdapters
{
    public static class RecentlyVisitedCompanyDataAdapter
    {
        public static RecentlyVisitedCompany ToDomainObject(this RecentlyVisitedCompanyEntity entity)
        {
            return new RecentlyVisitedCompany
            {
                Symbol = entity.Symbol,
                Name = entity.Symbol,
                Currency = entity.Currency,
                StockExchange = entity.StockExchange,
                ExchangeShortName = entity.ExchangeShortName,
                LastVisited = entity.LastVisited
            };
        }

        public static RecentlyVisitedCompanyEntity ToEntity(this RecentlyVisitedCompany recentlyVisitedCompany)
        {
            return new RecentlyVisitedCompanyEntity
            {
                Symbol = recentlyVisitedCompany.Symbol,
                Name = recentlyVisitedCompany.Symbol,
                Currency = recentlyVisitedCompany.Currency,
                StockExchange = recentlyVisitedCompany.StockExchange,
                ExchangeShortName = recentlyVisitedCompany.ExchangeShortName,
                LastVisited = recentlyVisitedCompany.LastVisited
            };
        }
    }
}
