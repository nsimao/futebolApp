using FutebolAPP.App.Services;
using FutebolAPP.App.ViewModels;
using Xamarin.Forms;

namespace FutebolAPP.App
{
    public partial class MainPage
    {
        private MainViewModel ViewModel => BindingContext as MainViewModel;

        public MainPage()
        {
            InitializeComponent();
            var futebolApiService = DependencyService.Get<IFutebolApiService>();
            BindingContext = new MainViewModel(futebolApiService);
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                ViewModel.ShowLigaCommand.Execute(e.SelectedItem);
            }
        }
    }
}
