using FinStat.Domain.Enums;
using FinStat.Resources.Localization;

namespace FinStat.Mobile.ViewModels
{
    public class ExchangeViewModel
    {
        public ExchangeViewModel(Exchange value)
        {
            Value = value;
            Text = Loc.Text(value);
        }

        public Exchange Value { get; }

        public string Text { get; }
    }
}
