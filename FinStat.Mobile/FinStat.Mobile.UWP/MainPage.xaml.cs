using FinStat.Mobile.UWP.Configuration;
using Application = FinStat.Mobile.App;

namespace FinStat.Mobile.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            var bootstrapper = new UwpBootstrapper();
            LoadApplication(new Application(bootstrapper));
        }
    }
}
