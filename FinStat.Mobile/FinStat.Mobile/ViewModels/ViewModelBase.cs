using System;
using System.Threading.Tasks;
using System.Windows.Input;
using FinStat.Common.Utils;
using FinStat.Mobile.Commands;
using FinStat.Mobile.Extensions;
using FinStat.Resources.Localization;
using Prism.Mvvm;
using Prism.Navigation;

namespace FinStat.Mobile.ViewModels
{
    public abstract class ViewModelBase : BindableBase, INavigatedAware, IDisposable
    {
        private bool _canGoBack;
        private bool _hasTitleBar;
        private bool _noAccessToContent;
        private bool _hasBottomNavigation;
        private string _title;
        private string _indicatorCaption;

        private bool _disposed;

        protected ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;

            OperationScope = new AsyncOperationScope();

            HasTitleBar = false;
            CanGoBack = false;

            BottomNavigation = new BottomNavigationViewModel(navigationService);
            IndicatorCaption = Loc.Text(TranslationKeys.ActivityIndicatorCaptionText);

            NavigateBackCommand = new AsyncCommand(ExecuteNavigateBackCommandAsync);
        }

        public bool CanGoBack
        {
            get => _canGoBack;
            protected set => SetProperty(ref _canGoBack, value);
        }

        public bool HasTitleBar
        {
            get => _hasTitleBar;
            protected set => SetProperty(ref _hasTitleBar, value);
        }

        public bool NoAccessToContent
        {
            get => _noAccessToContent;
            protected set => SetProperty(ref _noAccessToContent, value);
        }

        public bool HasBottomNavigation
        {
            get => _hasBottomNavigation;
            protected set => SetProperty(ref _hasBottomNavigation, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string IndicatorCaption
        {
            get => _indicatorCaption;
            set => SetProperty(ref _indicatorCaption, value);
        }

        protected INavigationService NavigationService { get; }

        public ICommand NavigateBackCommand { get; }

        public BottomNavigationViewModel BottomNavigation { get; private set; }

        public AsyncOperationScope OperationScope { get; }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            await LoadDataAsync(parameters).ConfigureAwait(false);
        }

        protected virtual async Task LoadDataAsync(INavigationParameters navigationParameters)
        {
            await Task.CompletedTask.ConfigureAwait(false);
        }

        private async Task ExecuteNavigateBackCommandAsync()
        {
            await NavigationService.GoBackWithoutAnimationAsync().ConfigureAwait(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                DisposeInternal();
            }

            _disposed = true;
        }

        protected virtual void DisposeInternal() { }
    }
}
