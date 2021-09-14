using System;
using SQLite;

namespace FinStat.DataAccess.Entities
{
    [Table("RecentlyVisitedCompany")]
    public class RecentlyVisitedCompanyEntity
    {
        [PrimaryKey]
        public string Symbol { get; set; }

        public string Name { get; set; }

        public string Currency { get; set; }

        public string StockExchange { get; set; }

        public string ExchangeShortName { get; set; }

        public DateTime LastVisited { get; set; }
    }
}
