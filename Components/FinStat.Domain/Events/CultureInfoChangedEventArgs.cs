using System;
using System.Globalization;

namespace FinStat.Domain.Events
{
    public class CultureInfoChangedEventArgs : EventArgs
    {
        public CultureInfoChangedEventArgs(CultureInfo cultureInfo)
        {
            CultureInfo = cultureInfo;
        }

        public CultureInfo CultureInfo { get; }
    }
}
