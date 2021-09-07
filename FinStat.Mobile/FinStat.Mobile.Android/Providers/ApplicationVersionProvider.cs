using Android.Content.PM;
using FinStat.Domain.Interfaces.Required;
using Plugin.CurrentActivity;
using Plugin.LatestVersion;

namespace FinStat.Mobile.Droid.Providers
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
            var context = CrossCurrentActivity.Current.AppContext;
            if (context?.PackageManager == null || context.PackageName == null)
                return "0";

            var packageInfo = context.PackageManager.GetPackageInfo(context.PackageName, PackageInfoFlags.MetaData);
            if (packageInfo == null)
                return "0";

            return packageInfo.LongVersionCode.ToString();
        }
    }
}