using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinStat.Mobile.Commands;
using FinStat.Mobile.Extensions;
using FinStat.Mobile.Navigation;
using FinStat.Mobile.ViewModels.DataGrid;
using Prism.Navigation;

namespace FinStat.Mobile.ViewModels
{
    public class StatementPageViewModelBase : ViewModelBase
    {
        private IEnumerable<RowViewModel> _rows;

        public StatementPageViewModelBase(INavigationService navigationService)
            : base(navigationService)
        {
            DoubleTappedCommand = new AsyncCommand<RowViewModel>(ExecuteDoubleTappedCommandAsync);
        }

        public bool NoDataToPlot => Rows == null || !Rows.Any();

        public IEnumerable<RowViewModel> Rows
        {
            get => _rows;
            set
            {
                if (SetProperty(ref _rows, value))
                {
                    RaisePropertyChanged(nameof(NoDataToPlot));
                }
            }
        }

        public IAsyncCommand<RowViewModel> DoubleTappedCommand { get; }

        private Task ExecuteDoubleTappedCommandAsync(RowViewModel rowViewModel)
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add<RowViewModel>(rowViewModel);
            return NavigationService.NavigateWithoutAnimationAsync(Pages.Chart, navigationParameters);
        }
    }
}
