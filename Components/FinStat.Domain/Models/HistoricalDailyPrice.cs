namespace FinStat.Domain.Models
{
    public class HistoricalDailyPrice
    {
        public string Symbol { get; set; }

        public HistoricalStockPrice[] Historical { get; set; }
    }
}
