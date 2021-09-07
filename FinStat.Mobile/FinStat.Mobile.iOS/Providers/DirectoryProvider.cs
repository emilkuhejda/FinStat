using System;
using System.IO;
using FinStat.Domain.Interfaces.Required;

namespace FinStat.Mobile.iOS.Providers
{
    public class DirectoryProvider : IDirectoryProvider
    {
        public string GetPath()
        {
            const string libraryName = "Library";

            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(documentsPath, "..", libraryName);
        }
    }
}