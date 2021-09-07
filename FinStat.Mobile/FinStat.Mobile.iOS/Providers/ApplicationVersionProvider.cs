using FinStat.Domain.Interfaces.Required;
using Foundation;
using Plugin.LatestVersion;

namespace FinStat.Mobile.iOS.Providers
{
    public class ApplicationVersionProvider : IApplicationVersionProvider
    {
        public string GetInstalledVersionNumber()
        {
            try
            {
                return $"{CrossLatestVersion.Current.InstalledVersionNumber}.{GetBuildNumber()}";
            }
            catch
            {
                return "0.0.0";
            }
        }

        private string GetBuildNumber()
        {
            using (var bundleVersionString = new NSString("CFBundleVersion"))
            {
                return NSBundle.MainBundle.InfoDictionary.ValueForKey(bundleVersionString).ToString();
            }
        }
    }
}