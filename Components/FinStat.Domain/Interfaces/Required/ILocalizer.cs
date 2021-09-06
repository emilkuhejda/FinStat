using System;
using System.Globalization;
using FinStat.Domain.Events;

namespace FinStat.Domain.Interfaces.Required
{
    public interface ILocalizer
    {
        event EventHandler<CultureInfoChangedEventArgs> CultureInfoChanged;

        CultureInfo GetCurrentCulture();

        void SetCultureInfo(CultureInfo cultureInfo);
    }
}
