using FFImageLoading.Forms.Platform;
using FinStat.Mobile.iOS.Configuration;
using Foundation;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.XForms.iOS.Buttons;
using UIKit;
using Xamarin.Forms;

namespace FinStat.Mobile.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Initialize();
            InitializeSyncfusionControls();

            var bootstrapper = new OsxBootstrapper();
            var application = new App(bootstrapper);
            LoadApplication(application);

            return base.FinishedLaunching(app, options);
        }

        private void Initialize()
        {
            Forms.Init();

            CachedImageRenderer.Init();
        }

        private void InitializeSyncfusionControls()
        {
            SfSegmentedControlRenderer.Init();

            using (var busyIndicatorRenderer = new SfBusyIndicatorRenderer()) { }
        }
    }
}
