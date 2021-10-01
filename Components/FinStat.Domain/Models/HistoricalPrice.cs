namespace FinStat.Domain.Models
{
    public class HistoricalPrice
    {
        public string Date { get; set; }

        public double Open { get; set; }

        public double Low { get; set; }

        public double High { get; set; }

        public double Close { get; set; }

        public int Volume { get; set; }
    }
}
