namespace FinStat.Domain.Interfaces.Configuration
{
    public interface IApplicationSettings
    {
        string ApiKey { get; }

        string SyncfusionKey { get; }

        string SupportMailAddress { get; }

        int SearchLimit { get; }

        int StatementsLimit { get; }
    }
}
