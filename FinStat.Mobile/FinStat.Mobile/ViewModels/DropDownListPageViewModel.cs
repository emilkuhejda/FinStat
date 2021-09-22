using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FinStat.Common.Utils;
using FinStat.Mobile.Commands;
using FinStat.Mobile.Extensions;
using FinStat.Mobile.Navigation.Parameters;
using Prism.Navigation;

namespace FinStat.Mobile.ViewModels
{
    public class DropDownListPageViewModel : ViewModelBase
    {
        private IEnumerable<DropDownListViewModel> _items;
        private DropDownListViewModel _selectedItem;

        public DropDownListPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            CanGoBack = true;
            HasTitleBar = true;

            SelectedItemChangedCommand = new AsyncCommand(ExecuteSelectedItemChangedCommandAsync, CanExecuteSelectedItemChangedCommand);
        }

        public IEnumerable<DropDownListViewModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public DropDownListViewModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public ICommand SelectedItemChangedCommand { get; }

        protected override async Task LoadDataAsync(INavigationParameters navigationParameters)
        {
            using (new OperationMonitor(OperationScope))
            {
                var dropDownListNavigationParameters = navigationParameters.GetValue<DropDownListNavigationParameters>();
                Title = dropDownListNavigationParameters.Title;
                Items = dropDownListNavigationParameters.Items.ToList();
                SelectedItem = Items.SingleOrDefault(x => x.IsSelected);

                await Task.CompletedTask;
            }
        }

        private bool CanExecuteSelectedItemChangedCommand()
        {
            return SelectedItem != null;
        }

        private async Task ExecuteSelectedItemChangedCommandAsync()
        {
            var navigationParameter = new NavigationParameters();
            navigationParameter.Add<DropDownListViewModel>(SelectedItem);

            await NavigationService.GoBackWithoutAnimationAsync(navigationParameter).ConfigureAwait(false);
        }
    }
}
