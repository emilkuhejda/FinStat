using FinStat.Mobile.ViewModels;
using Syncfusion.XForms.TabView;
using Xamarin.Forms.Xaml;

namespace FinStat.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatementsPage
    {
        public StatementsPage()
        {
            InitializeComponent();
        }

        private async void OnTabViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tabView = sender as SfTabView;
            var statementsPageViewModel = tabView?.BindingContext as StatementsPageViewModel;
            if (statementsPageViewModel == null)
                return;

            await statementsPageViewModel.TabItemChangedAsync(e.Index);
        }
    }
}