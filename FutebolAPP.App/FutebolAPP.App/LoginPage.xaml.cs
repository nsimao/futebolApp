using FutebolAPP.App.ViewModels;

namespace FutebolAPP.App
{
    public partial class LoginPage
    {
        private LoginViewModel ViewModel => BindingContext as LoginViewModel;

        public LoginPage()
        {
            InitializeComponent();
        }
    }
}