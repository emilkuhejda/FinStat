using System.Collections.Generic;

namespace FinStat.Mobile.ViewModels.DataGrid
{
    public class RowViewModel
    {
        public RowViewModel(IEnumerable<CellViewModel> cells)
        {
            Cells = cells;
        }

        public IEnumerable<CellViewModel> Cells { get; }
    }
}
