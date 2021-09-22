using System;
using System.Threading.Tasks;
using System.Windows.Input;
using FinStat.Common.Utils;
using FinStat.Domain.Enums;
using FinStat.Domain.Http;
using FinStat.Mobile.Commands;
using FinStat.Mobile.Extensions;
using FinStat.Mobile.Utils;
using FinStat.Resources.Localization;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

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
            await LoadDataAsync(parameters);
        }

        protected async Task<(bool success, T payload)> HandleWebCallAsync<T>(Func<Task<HttpRequestResult<T>>> action) where T : class
        {
            var httpRequestResult = await action();
            if (httpRequestResult.State == HttpRequestState.Success)
                return (true, httpRequestResult.Payload);

            if (httpRequestResult.State == HttpRequestState.Offline)
                await DisplayAlertAsync(string.Empty, Loc.Text(TranslationKeys.OfflineErrorMessage));

            await DisplayAlertAsync(string.Empty, Loc.Text(TranslationKeys.GeneralErrorMessage));
            return (false, null);
        }

        protected Task DisplayAlertAsync(string title, string message)
        {
            return ThreadHelper.InvokeOnUiThread(() => DisplayAlertAsync(title, message, Loc.Text(TranslationKeys.Ok)));
        }

        protected Task DisplayAlertAsync(string title, string message, string ok)
        {
            return ThreadHelper.InvokeOnUiThread(() => Application.Current.MainPage.DisplayAlert(title, message, ok));
        }

        protected Task<bool> DisplayAlertAsync(string title, string message, string ok, string cancel)
        {
            return ThreadHelper.InvokeOnUiThread(() => Application.Current.MainPage.DisplayAlert(title, message, ok, cancel));
        }

        protected virtual async Task LoadDataAsync(INavigationParameters navigationParameters)
        {
            await Task.CompletedTask;
        }

        private async Task ExecuteNavigateBackCommandAsync()
        {
            await NavigationService.GoBackWithoutAnimationAsync();
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
