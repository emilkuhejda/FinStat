using System;
using FinStat.Domain.Enums;

namespace FinStat.Business.Extensions
{
    public static class ExchangeExtensions
    {
        public static string ToValue(this Exchange exchange)
        {
            switch (exchange)
            {
                case Exchange.Etf:
                    return "ETF";
                case Exchange.MutualFund:
                    return "MUTUAL_FUND";
                case Exchange.Commodity:
                    return "COMMODITY";
                case Exchange.Index:
                    return "INDEX";
                case Exchange.Crypto:
                    return "CRYPTO";
                case Exchange.Forex:
                    return "FOREX";
                case Exchange.Tsx:
                    return "TSX";
                case Exchange.Amex:
                    return "AMEX";
                case Exchange.Nasdaq:
                    return "NASDAQ";
                case Exchange.Nyse:
                    return "NYSE";
                case Exchange.Euronext:
                    return "EURONEXT";
                case Exchange.Xetra:
                    return "XETRA";
                case Exchange.Nse:
                    return "NSE";
                case Exchange.Lse:
                    return "LSE";
                default:
                    throw new ArgumentOutOfRangeException(nameof(exchange));
            }
        }
    }
}
