using FinStat.Domain.Interfaces.Configuration;

namespace FinStat.Business.Configuration
{
    public class ApplicationSettings : IApplicationSettings
    {
        public string ApiKey { get; } = "263df291e3b2431275f5bb230f88defb";

        public string SyncfusionKey { get; } = "NDk3NjQ1QDMxMzkyZTMyMmUzMElmeGJoMDJYN1lnZ0p4VTRJSkhSRExTMWN2TUdablBjMmNQTDV6VkJ0dFE9";

        public string SupportMailAddress { get; } = "emil.kuhejda@gmail.com";

        public int SearchLimit { get; } = 20;

        public int StatementsLimit { get; } = 10;
    }
}
