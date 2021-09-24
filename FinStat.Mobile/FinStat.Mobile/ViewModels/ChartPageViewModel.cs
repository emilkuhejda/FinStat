using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinStat.Common.Utils;
using FinStat.Mobile.Extensions;
using FinStat.Mobile.ViewModels.DataGrid;
using FinStat.Resources.Localization;
using Prism.Navigation;
using Syncfusion.SfChart.XForms;

namespace FinStat.Mobile.ViewModels
{
    public class ChartPageViewModel : ViewModelBase
    {
        private IEnumerable<ChartDataPoint> _chartData = new List<ChartDataPoint>();

        public ChartPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            CanGoBack = true;
            HasTitleBar = true;
            Title = Loc.Text(TranslationKeys.NoAvailableData);
        }

        public IEnumerable<ChartDataPoint> ChartData
        {
            get => _chartData;
            set
            {
                if (SetProperty(ref _chartData, value))
                {
                    RaisePropertyChanged(nameof(HasDataToPlot));
                }
            }
        }

        public bool HasDataToPlot => ChartData.Any();

        protected override async Task LoadDataAsync(INavigationParameters navigationParameters)
        {
            await Task.CompletedTask;

            using (new OperationMonitor(OperationScope))
            {
                var rowViewModel = navigationParameters.GetValue<RowViewModel>();
                var cellViewModels = rowViewModel.Cells.ToList();
                if (!cellViewModels.Any())
                    return;

                var titleCell = cellViewModels.FirstOrDefault();
                if (titleCell != null)
                {
                    Title = titleCell.Value;
                }

                if (cellViewModels.Count < 2)
                    return;

                ChartData = cellViewModels.Skip(1).Reverse().Select(x => new ChartDataPoint(x.Title, Convert.ToDouble(x.Value.Replace("%", string.Empty))));
            }
        }
    }
}
