using System;
using System.Globalization;
using FinStat.Domain.Events;
using FinStat.Domain.Interfaces.Required;

namespace FinStat.Mobile.UWP.Localization
{
    public class Localizer : ILocalizer
    {
        public event EventHandler<CultureInfoChangedEventArgs> CultureInfoChanged;

        public CultureInfo GetCurrentCulture()
        {
            return CultureInfo.CurrentCulture;
        }

        public void SetCultureInfo(CultureInfo cultureInfo)
        {
            if (cultureInfo == null)
                throw new ArgumentNullException(nameof(cultureInfo));

            if (CultureInfo.CurrentCulture != cultureInfo || CultureInfo.CurrentUICulture != cultureInfo)
            {
                CultureInfo.CurrentCulture = cultureInfo;
                CultureInfo.CurrentUICulture = cultureInfo;

                CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

                OnCultureInfoChanged(cultureInfo);
            }
        }

        private void OnCultureInfoChanged(CultureInfo cultureInfo)
        {
            CultureInfoChanged?.Invoke(this, new CultureInfoChangedEventArgs(cultureInfo));
        }
    }
}
