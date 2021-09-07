using System;
using FinStat.Domain.Interfaces.Required;

namespace FinStat.Mobile.Droid.Providers
{
    public class DirectoryProvider : IDirectoryProvider
    {
        public string GetPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }
    }
}