using System;

namespace FinStat.Domain.Models
{
    public class HistoricalStockPrice
    {
        public DateTime Date { get; set; }

        public double Close { get; set; }
    }
}
