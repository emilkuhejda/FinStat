using System;

namespace FinStat.Domain.Models
{
    public class RecentlyVisitedCompany
    {
        public string Symbol { get; set; }

        public string Name { get; set; }

        public string Currency { get; set; }

        public string StockExchange { get; set; }

        public string ExchangeShortName { get; set; }

        public DateTime LastVisited { get; set; }
    }
}
