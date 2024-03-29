﻿using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FinStat.Common.Utils;
using FinStat.Domain.Configuration;
using FinStat.Domain.Enums;
using FinStat.Domain.Interfaces.Configuration;
using FinStat.Domain.Interfaces.Required;
using FinStat.Domain.Interfaces.Services;
using FinStat.Mobile.Commands;
using FinStat.Mobile.Extensions;
using FinStat.Mobile.Navigation;
using FinStat.Mobile.Navigation.Parameters;
using FinStat.Resources.Localization;
using Prism.Navigation;
using Xamarin.Forms;

namespace FinStat.Mobile.ViewModels
{
    public class MorePageViewModel : ViewModelBase
    {
        private readonly IEmailService _emailService;
        private readonly IInternalValueService _internalValueService;
        private readonly IApplicationVersionProvider _applicationVersionProvider;
        private readonly IApplicationSettings _applicationSettings;

        private DisplayUnit _selectedDisplayUnit;
        private string _version;

        public MorePageViewModel(
            IEmailService emailService,
            IInternalValueService internalValueService,
            IApplicationVersionProvider applicationVersionProvider,
            IApplicationSettings applicationSettings,
            INavigationService navigationService)
            : base(navigationService)
        {
            _emailService = emailService;
            _internalValueService = internalValueService;
            _applicationVersionProvider = applicationVersionProvider;
            _applicationSettings = applicationSettings;

            HasTitleBar = true;
            HasBottomNavigation = true;

            Title = Loc.Text(TranslationKeys.More);

            NavigateToDisplayUnitCommand = new AsyncCommand(ExecuteNavigateToDisplayUnitCommandAsync);
            NavigateToEmailCommand = new AsyncCommand(ExecuteNavigateToEmailCommandAsync);
        }

        public DisplayUnit SelectedDisplayUnit
        {
            get => _selectedDisplayUnit;
            set => SetProperty(ref _selectedDisplayUnit, value);
        }

        public string Version
        {
            get => _version;
            set => SetProperty(ref _version, value);
        }

        public ICommand NavigateToDisplayUnitCommand { get; }

        public ICommand NavigateToEmailCommand { get; }

        protected override async Task LoadDataAsync(INavigationParameters navigationParameters)
        {
            using (new OperationMonitor(OperationScope))
            {
                Version = _applicationVersionProvider.GetInstalledVersionNumber();

                if (navigationParameters.GetNavigationMode() == NavigationMode.New)
                {
                    SelectedDisplayUnit = await _internalValueService.GetValueAsync(InternalValues.DefaultDisplayUnit);
                }
                else if (navigationParameters.GetNavigationMode() == NavigationMode.Back)
                {
                    var dropDownListViewModel = navigationParameters.GetValue<DropDownListViewModel>();
                    await HandleSelectionAsync(dropDownListViewModel);
                }
            }
        }

        private Task HandleSelectionAsync(DropDownListViewModel dropDownListViewModel)
        {
            if (dropDownListViewModel == null)
                return Task.CompletedTask;

            switch (dropDownListViewModel.Type)
            {
                case nameof(SelectedDisplayUnit):
                    return HandleDisplayUnitSelectionAsync((DisplayUnit)dropDownListViewModel.Value);
                default:
                    throw new NotSupportedException(nameof(dropDownListViewModel.Type));
            }
        }

        private Task HandleDisplayUnitSelectionAsync(DisplayUnit displayUnit)
        {
            SelectedDisplayUnit = displayUnit;

            return _internalValueService.UpdateValueAsync(InternalValues.DefaultDisplayUnit, displayUnit);
        }

        private Task ExecuteNavigateToDisplayUnitCommandAsync()
        {
            var displayUnits = Enum.GetValues(typeof(DisplayUnit)).Cast<DisplayUnit>();
            var viewModels = displayUnits.Select(x => new DropDownListViewModel
            {
                Text = Loc.Text(x),
                Value = x,
                Type = nameof(SelectedDisplayUnit),
                IsSelected = x == SelectedDisplayUnit
            });

            var navigationParameters = new NavigationParameters();
            var parameters = new DropDownListNavigationParameters(Loc.Text(TranslationKeys.DisplayUnit), viewModels);
            navigationParameters.Add<DropDownListNavigationParameters>(parameters);

            return NavigationService.NavigateWithoutAnimationAsync(Pages.DropDownListPage, navigationParameters);
        }

        private Task ExecuteNavigateToEmailCommandAsync()
        {
            if (string.IsNullOrWhiteSpace(_applicationSettings.SupportMailAddress))
                return Task.CompletedTask;

            var subject = $"{Loc.Text(TranslationKeys.ApplicationTitle)}";
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss \"GMT\"zzz", CultureInfo.InvariantCulture);
            var message = new StringBuilder()
                .AppendLine()
                .AppendLine()
                .AppendLine()
                .AppendLine()
                .AppendLine()
                .AppendLine()
                .AppendLine()
                .AppendLine("_______________________________________")
                .AppendLine(Loc.Text(TranslationKeys.ContactUsApplicationVersion, _applicationVersionProvider.GetInstalledVersionNumber(), Device.RuntimePlatform))
                .AppendLine(Loc.Text(TranslationKeys.ContactUsTimeStamp, timestamp))
                .ToString();

            return _emailService.SendAsync(_applicationSettings.SupportMailAddress, subject, message);
        }
    }
}
