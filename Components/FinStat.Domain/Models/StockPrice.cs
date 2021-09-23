namespace FinStat.Domain.Models
{
    public class StockPrice
    {
        public string Symbol { get; set; }

        public double Price { get; set; }

        public long Volume { get; set; }
    }
}
