using System;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FinStat.Common.Utils;
using FinStat.Domain.Interfaces.Configuration;
using FinStat.Domain.Interfaces.Required;
using FinStat.Domain.Interfaces.Services;
using FinStat.Mobile.Commands;
using FinStat.Resources.Localization;
using Prism.Navigation;
using Xamarin.Forms;

namespace FinStat.Mobile.ViewModels
{
    public class MorePageViewModel : ViewModelBase
    {
        private readonly IEmailService _emailService;
        private readonly IApplicationVersionProvider _applicationVersionProvider;
        private readonly IApplicationSettings _applicationSettings;

        private string _version;

        public MorePageViewModel(
            IEmailService emailService,
            IApplicationVersionProvider applicationVersionProvider,
            IApplicationSettings applicationSettings,
            INavigationService navigationService)
            : base(navigationService)
        {
            _emailService = emailService;
            _applicationVersionProvider = applicationVersionProvider;
            _applicationSettings = applicationSettings;

            HasTitleBar = true;
            HasBottomNavigation = true;

            Title = Loc.Text(TranslationKeys.ApplicationTitle);

            NavigateToEmailCommand = new AsyncCommand(ExecuteNavigateToEmailCommandAsync);
        }

        public string Version
        {
            get => _version;
            set => SetProperty(ref _version, value);
        }

        public ICommand NavigateToEmailCommand { get; }

        protected override async Task LoadDataAsync(INavigationParameters navigationParameters)
        {
            using (new OperationMonitor(OperationScope))
            {
                Version = _applicationVersionProvider.GetInstalledVersionNumber();

                await Task.CompletedTask;
            }
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
