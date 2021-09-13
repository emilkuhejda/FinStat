﻿using System.Threading.Tasks;
using System.Windows.Input;
using FinStat.Common.Utils;
using FinStat.Domain.Interfaces.Configuration;
using FinStat.Domain.Interfaces.Services;
using FinStat.Domain.Models;
using FinStat.Mobile.Commands;
using FinStat.Mobile.Extensions;
using FinStat.Resources.Localization;
using Prism.Navigation;

namespace FinStat.Mobile.ViewModels
{
    public class StatementsPageViewModel : ViewModelBase
    {
        private IncomeStatementPageViewModel _incomeStatementPage;
        private int _selectedIndex;
        private bool _annualData;
        private bool _quarterlyData;
        private string _displayUnitsText;

        public StatementsPageViewModel(
            IWebService webService,
            IApplicationSettings applicationSettings,
            INavigationService navigationService)
            : base(navigationService)
        {
            CanGoBack = true;
            HasTitleBar = true;
            HasBottomNavigation = false;

            AnnualData = true;
            QuarterlyData = false;

            DisplayUnitsText = Loc.Text(TranslationKeys.AllNumbersInUnit, Loc.Text(applicationSettings.DisplayUnit));

            IncomeStatementPage = new IncomeStatementPageViewModel(webService, applicationSettings, navigationService);

            LoadAnnualDataCommand = new AsyncCommand(ExecuteLoadAnnualDataCommandAsync);
            LoadQuarterlyDataCommand = new AsyncCommand(ExecuteLoadQuarterlyDataCommandAsync);
        }

        private SearchResult SearchResult { get; set; }

        public IncomeStatementPageViewModel IncomeStatementPage
        {
            get => _incomeStatementPage;
            set => SetProperty(ref _incomeStatementPage, value);
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set => SetProperty(ref _selectedIndex, value);
        }

        public bool AnnualData
        {
            get => _annualData;
            set => SetProperty(ref _annualData, value);
        }

        public bool QuarterlyData
        {
            get => _quarterlyData;
            set => SetProperty(ref _quarterlyData, value);
        }

        public string DisplayUnitsText
        {
            get => _displayUnitsText;
            set => SetProperty(ref _displayUnitsText, value);
        }

        public ICommand LoadAnnualDataCommand { get; }

        public ICommand LoadQuarterlyDataCommand { get; }

        protected override Task LoadDataAsync(INavigationParameters navigationParameters)
        {
            SearchResult = navigationParameters.GetValue<SearchResult>();
            Title = SearchResult.Name;

            return InitializeAsync();
        }

        private Task ExecuteLoadAnnualDataCommandAsync()
        {
            if (!AnnualData)
            {
                AnnualData = true;
                return Task.CompletedTask;
            }

            QuarterlyData = false;
            return InitializeAsync();
        }

        private Task ExecuteLoadQuarterlyDataCommandAsync()
        {
            if (!QuarterlyData)
            {
                QuarterlyData = true;
                return Task.CompletedTask;
            }

            AnnualData = false;
            return InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            using (new OperationMonitor(OperationScope))
            {
                await IncomeStatementPage.InitializeAsync(SearchResult, QuarterlyData).ConfigureAwait(false);
            }
        }
    }
}