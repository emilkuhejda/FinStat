using FinStat.Mobile.UWP.Configuration;
using Syncfusion.ListView.XForms.UWP;
using Application = FinStat.Mobile.App;

namespace FinStat.Mobile.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            InitializeSyncfusionControls();

            var bootstrapper = new UwpBootstrapper();
            LoadApplication(new Application(bootstrapper));
        }

        private void InitializeSyncfusionControls()
        {
            SfListViewRenderer.Init();
        }
    }
}
