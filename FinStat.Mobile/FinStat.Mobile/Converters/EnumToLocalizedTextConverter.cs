using System;
using System.Globalization;
using FinStat.Resources.Localization;
using Xamarin.Forms;

namespace FinStat.Mobile.Converters
{
    public class EnumToLocalizedTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Enum))
                return string.Empty;

            var enumValue = (Enum)value;
            return Loc.Text(enumValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
