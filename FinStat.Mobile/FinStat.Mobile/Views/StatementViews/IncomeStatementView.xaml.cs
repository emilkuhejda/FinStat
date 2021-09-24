using System.Collections.Generic;
using System.Linq;
using FinStat.Mobile.ViewModels;
using FinStat.Mobile.ViewModels.DataGrid;
using Syncfusion.SfDataGrid.XForms;
using Xamarin.Forms.Xaml;

namespace FinStat.Mobile.Views.StatementViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncomeStatementView
    {
        public IncomeStatementView()
        {
            InitializeComponent();
        }

        private void DataGridOnItemsSourceChanged(object sender, GridItemsSourceChangedEventArgs e)
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

        private async void DataGridOnGridDoubleTapped(object sender, GridDoubleTappedEventArgs e)
        {
            if (e.RowColumnIndex.RowIndex == 0)
                return;

            if (!(BindingContext is StatementsPageViewModel pageViewModel))
                return;

            if (!(e.RowData is RowViewModel rowData))
                return;

            await pageViewModel.IncomeStatementPage.DoubleTappedCommand.ExecuteAsync(rowData);
        }
    }
}