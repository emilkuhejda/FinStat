using System;

namespace FinStat.Domain.Models
{
    public class IncomeStatement
    {
        public DateTime Date { get; set; }

        public string Symbol { get; set; }

        public string ReportedCurrency { get; set; }

        public DateTime FillingDate { get; set; }

        public DateTime AcceptedDate { get; set; }

        public string Period { get; set; }

        public long Revenue { get; set; }

        public long CostOfRevenue { get; set; }

        public long GrossProfit { get; set; }

        public double GrossProfitRatio { get; set; }

        public long ResearchAndDevelopmentExpenses { get; set; }

        public long GeneralAndAdministrativeExpenses { get; set; }

        public double SellingAndMarketingExpenses { get; set; }

        public long SellingGeneralAndAdministrativeExpenses { get; set; }

        public double OtherExpenses { get; set; }

        public long OperatingExpenses { get; set; }

        public long CostAndExpenses { get; set; }

        public long InterestExpense { get; set; }

        public long DepreciationAndAmortization { get; set; }

        public long Ebitda { get; set; }

        public double Ebitdaratio { get; set; }

        public long OperatingIncome { get; set; }

        public double OperatingIncomeRatio { get; set; }

        public long TotalOtherIncomeExpensesNet { get; set; }

        public long IncomeBeforeTax { get; set; }

        public double IncomeBeforeTaxRatio { get; set; }

        public long IncomeTaxExpense { get; set; }

        public long NetIncome { get; set; }

        public double NetIncomeRatio { get; set; }

        public double Eps { get; set; }

        public double Epsdiluted { get; set; }

        public long WeightedAverageShsOut { get; set; }

        public long WeightedAverageShsOutDil { get; set; }

        public string Link { get; set; }

        public string FinalLink { get; set; }
    }
}
