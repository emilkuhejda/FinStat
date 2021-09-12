namespace FinStat.Mobile.ViewModels.DataGrid
{
    public class CellViewModel
    {
        public CellViewModel(string title, string value)
        {
            Title = title;
            Value = value;
        }

        public string Title { get; }

        public string Value { get; }
    }
}
