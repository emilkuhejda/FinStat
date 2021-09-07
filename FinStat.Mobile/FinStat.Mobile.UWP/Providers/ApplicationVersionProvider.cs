using FinStat.Domain.Interfaces.Required;
using Plugin.LatestVersion;

namespace FinStat.Mobile.UWP.Providers
{
    public class ApplicationVersionProvider : IApplicationVersionProvider
    {
        public string GetInstalledVersionNumber()
        {
            try
            {
                return CrossLatestVersion.Current.InstalledVersionNumber;
            }
            catch
            {
                return "0.0.0";
            }
        }
    }
}
