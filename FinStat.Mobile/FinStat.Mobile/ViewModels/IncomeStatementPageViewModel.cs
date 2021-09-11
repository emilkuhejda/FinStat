using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinStat.Common.Utils;
using FinStat.Domain.Interfaces.Services;
using FinStat.Domain.Models;
using FinStat.Mobile.Extensions;
using Prism.Navigation;

namespace FinStat.Mobile.ViewModels
{
    public class IncomeStatementPageViewModel : ViewModelBase
    {
        private readonly IWebService _webService;
        private IEnumerable<IncomeStatement> _incomeStatements;

        public IncomeStatementPageViewModel(
            IWebService webService,
            INavigationService navigationService)
            : base(navigationService)
        {
            _webService = webService;

            CanGoBack = true;
            HasTitleBar = true;
            HasBottomNavigation = true;
        }

        public IEnumerable<IncomeStatement> IncomeStatements
        {
            get => _incomeStatements;
            set
            {
                if (SetProperty(ref _incomeStatements, value))
                {
                    RaisePropertyChanged(nameof(NoDataToPlot));
                }
            }
        }

        public bool NoDataToPlot => IncomeStatements == null || !IncomeStatements.Any();

        protected override async Task LoadDataAsync(INavigationParameters navigationParameters)
        {
            using (new OperationMonitor(OperationScope))
            {
                var searchResult = navigationParameters.GetValue<SearchResult>();
                Title = searchResult.Name;

                var result = await HandleWebCallAsync(() => _webService.GetIncomeStatementsAsync(searchResult.Symbol, false));
                if (result.success)
                {
                    IncomeStatements = result.payload;
                }
            }
        }
    }
}
