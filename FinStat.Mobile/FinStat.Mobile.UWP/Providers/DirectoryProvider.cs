using Windows.Storage;
using FinStat.Domain.Interfaces.Required;

namespace FinStat.Mobile.UWP.Providers
{
    public class DirectoryProvider : IDirectoryProvider
    {
        public string GetPath()
        {
            return ApplicationData.Current.LocalFolder.Path;
        }
    }
}
