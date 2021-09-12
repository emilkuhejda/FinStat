using System.Collections.Generic;
using System.Linq;
using FinStat.Mobile.ViewModels.DataGrid;
using Syncfusion.SfDataGrid.XForms;
using Xamarin.Forms.Xaml;

namespace FinStat.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncomeStatementPage
    {
        public IncomeStatementPage()
        {
            InitializeComponent();
        }

        private void SfDataGrid_OnItemsSourceChanged(object sender, GridItemsSourceChangedEventArgs e)
        {
            if (e.NewItemSource is IList<RowViewModel> rows)
            {
                if (!rows.Any())
                    return;

                DataGrid.Columns.Clear();
                var columns = rows.First().Cells.ToList();
                for (var i = 0; i < columns.Count; i++)
                {
                    DataGrid.Columns.Add(new GridTextColumn { MappingName = $"Cells[{i}].Value", HeaderText = columns[i].Title });
                }
            }
        }
    }
}