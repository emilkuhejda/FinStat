namespace FinStat.Domain.Models
{
    public class BalanceSheet
    {
        public string Date { get; set; }

        public string Symbol { get; set; }

        public string ReportedCurrency { get; set; }

        public string FillingDate { get; set; }

        public string AcceptedDate { get; set; }

        public string Period { get; set; }

        public long CashAndCashEquivalents { get; set; }

        public long ShortTermInvestments { get; set; }

        public long CashAndShortTermInvestments { get; set; }

        public long NetReceivables { get; set; }

        public long Inventory { get; set; }

        public long OtherCurrentAssets { get; set; }

        public long TotalCurrentAssets { get; set; }

        public long PropertyPlantEquipmentNet { get; set; }

        public double Goodwill { get; set; }

        public double IntangibleAssets { get; set; }

        public double GoodwillAndIntangibleAssets { get; set; }

        public long LongTermInvestments { get; set; }

        public double TaxAssets { get; set; }

        public long OtherNonCurrentAssets { get; set; }

        public long TotalNonCurrentAssets { get; set; }

        public long OtherAssets { get; set; }

        public long TotalAssets { get; set; }

        public long AccountPayables { get; set; }

        public long ShortTermDebt { get; set; }

        public double TaxPayables { get; set; }

        public long DeferredRevenue { get; set; }

        public long OtherCurrentLiabilities { get; set; }

        public long TotalCurrentLiabilities { get; set; }

        public long LongTermDebt { get; set; }

        public double DeferredRevenueNonCurrent { get; set; }

        public double DeferredTaxLiabilitiesNonCurrent { get; set; }

        public long OtherNonCurrentLiabilities { get; set; }

        public long TotalNonCurrentLiabilities { get; set; }

        public double OtherLiabilities { get; set; }

        public long TotalLiabilities { get; set; }

        public long CommonStock { get; set; }

        public long RetainedEarnings { get; set; }

        public long AccumulatedOtherComprehensiveIncomeLoss { get; set; }

        public double OthertotalStockholdersEquity { get; set; }

        public long TotalStockholdersEquity { get; set; }

        public long TotalLiabilitiesAndStockholdersEquity { get; set; }

        public long TotalInvestments { get; set; }

        public long TotalDebt { get; set; }

        public long NetDebt { get; set; }

        public string Link { get; set; }

        public string FinalLink { get; set; }
    }
}
