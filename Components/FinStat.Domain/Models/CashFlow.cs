using System;

namespace FinStat.Domain.Models
{
    public class CashFlow
    {
        public DateTime Date { get; set; }

        public string Symbol { get; set; }

        public string ReportedCurrency { get; set; }

        public DateTime FillingDate { get; set; }

        public DateTime AcceptedDate { get; set; }

        public string Period { get; set; }

        public long NetIncome { get; set; }

        public long DepreciationAndAmortization { get; set; }

        public long DeferredIncomeTax { get; set; }

        public long StockBasedCompensation { get; set; }

        public long ChangeInWorkingCapital { get; set; }

        public long AccountsReceivables { get; set; }

        public long Inventory { get; set; }

        public long AccountsPayables { get; set; }

        public long OtherWorkingCapital { get; set; }

        public long OtherNonCashItems { get; set; }

        public long NetCashProvidedByOperatingActivities { get; set; }

        public long InvestmentsInPropertyPlantAndEquipment { get; set; }

        public long AcquisitionsNet { get; set; }

        public long PurchasesOfInvestments { get; set; }

        public long SalesMaturitiesOfInvestments { get; set; }

        public long OtherInvestingActivites { get; set; }

        public long NetCashUsedForInvestingActivites { get; set; }

        public long DebtRepayment { get; set; }

        public long CommonStockIssued { get; set; }

        public long CommonStockRepurchased { get; set; }

        public long DividendsPaid { get; set; }

        public long OtherFinancingActivites { get; set; }

        public long NetCashUsedProvidedByFinancingActivities { get; set; }

        public double EffectOfForexChangesOnCash { get; set; }

        public long NetChangeInCash { get; set; }

        public long CashAtEndOfPeriod { get; set; }

        public long CashAtBeginningOfPeriod { get; set; }

        public long OperatingCashFlow { get; set; }

        public long CapitalExpenditure { get; set; }

        public long FreeCashFlow { get; set; }

        public string Link { get; set; }

        public string FinalLink { get; set; }
    }
}
