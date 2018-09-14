using FutebolAPP.App.Helpers;
using FutebolAPP.App.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FutebolAPP.App.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private IFutebolApiService _futebolApiService;

        public Command LoginCommand { get; }
        public Command LogoutCommand { get; }

        private bool _isLoggedIn = false;
        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set
            {
                if (SetProperty<bool>(ref _isLoggedIn, value))
                {
                    LoginCommand.ChangeCanExecute();
                    LogoutCommand.ChangeCanExecute();
                }
            }
        }

        private string _userID = string.Empty;
        public string UserID
        {
            get { return _userID; }
            set
            {
                if (SetProperty<string>(ref _userID, value))
                {
                    LoginCommand.ChangeCanExecute();
                    LogoutCommand.ChangeCanExecute();
                }
            }
        }

        private string _authToken = string.Empty;
        public string AuthToken
        {
            get { return _authToken; }
            set { SetProperty<string>(ref _authToken, value); }
        }

        private string _userFullName = string.Empty;
        public string UserFullName
        {
            get { return _userFullName; }
            set { SetProperty<string>(ref _userFullName, value); }
        }

        private string _userPictureURL = string.Empty;
        public string UserPictureURL
        {
            get { return _userPictureURL; }
            set { SetProperty<string>(ref _userPictureURL, value); }
        }

        public LoginViewModel(IFutebolApiService futebolApiService)
        {
            _futebolApiService = futebolApiService;

            Title = "Login";

            LoginCommand = new Command(ExecuteLoginCommand, CanExecuteLoginCommand);
            LogoutCommand = new Command(ExecuteLogoutCommand, CanExecuteLogoutCommand);

            LoadUserData();
        }

        private void LoadUserData()
        {
            UserID = Settings.UserId;
            AuthToken = Settings.AuthToken;
            UserFullName = Settings.UserFullName;
            UserPictureURL = Settings.UserPictureURL;
            IsLoggedIn = Settings.IsLoggedIn;
        }

        private async void ExecuteLoginCommand()
        {
            bool result = await _futebolApiService.LoginAsync();

            if (!result)
                await DisplayAlert("Ops...", "Erro ao realizar login!", "OK");

            LoadUserData();
        }

        private async void ExecuteLogoutCommand()
        {
            bool result = await _futebolApiService.LogoutAsync();

            if (!result)
                await DisplayAlert("Ops...", "Erro ao realizar logout!", "OK");

            LoadUserData();
        }

        private bool CanExecuteLoginCommand()
        {
            return !Settings.IsLoggedIn;
        }

        private bool CanExecuteLogoutCommand()
        {
            return Settings.IsLoggedIn;
        }
    }
}
