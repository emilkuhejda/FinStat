using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinStat.Common.Utils;
using FinStat.Mobile.Extensions;
using FinStat.Mobile.ViewModels.DataGrid;
using Prism.Navigation;
using Syncfusion.SfChart.XForms;

namespace FinStat.Mobile.ViewModels
{
    public class ChartPageViewModel : ViewModelBase
    {
        private IEnumerable<ChartDataPoint> _chartData;

        public ChartPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            CanGoBack = true;
            HasTitleBar = true;

            ChartData = new List<ChartDataPoint>();
        }

        public IEnumerable<ChartDataPoint> ChartData
        {
            get => _chartData;
            set => SetProperty(ref _chartData, value);
        }

        protected override async Task LoadDataAsync(INavigationParameters navigationParameters)
        {
            await Task.CompletedTask;

            using (new OperationMonitor(OperationScope))
            {
                var rowViewModel = navigationParameters.GetValue<RowViewModel>();
                var titleCell = rowViewModel.Cells.FirstOrDefault();
                if (titleCell != null)
                {
                    Title = titleCell.Value;
                }

                ChartData = rowViewModel.Cells.Skip(1).Reverse().Select(x => new ChartDataPoint(x.Title, Convert.ToDouble(x.Value.Replace("%", string.Empty))));
            }
        }
    }
}
