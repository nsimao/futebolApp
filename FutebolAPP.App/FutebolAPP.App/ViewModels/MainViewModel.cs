using FutebolAPP.App.Models;
using FutebolAPP.App.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FutebolAPP.App.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IFutebolApiService _futebolApiService;

        public ObservableCollection<Liga> Ligas { get; }

        public Command SobreCommand { get; }

        public Command LoginCommand { get; }

        public Command ShowLigaCommand { get; }

        public MainViewModel(IFutebolApiService futebolApiService)
        {
            _futebolApiService = futebolApiService;
            Ligas = new ObservableCollection<Liga>();
            SobreCommand = new Command(ExecuteSobreCommand);
            LoginCommand = new Command(ExecuteLoginCommand);
            ShowLigaCommand = new Command<Liga>(ExecuteShowLigaCommand);

            Title = "Futebol App";
        }

        private async void ExecuteSobreCommand()
        {
            await PushAsync<SobreViewModel>();
        }

        private async void ExecuteLoginCommand()
        {
            await PushAsync<LoginViewModel>(_futebolApiService);
        }

        private async void ExecuteShowLigaCommand(Liga liga)
        {
            await PushAsync<LigaDetalheViewModel>(_futebolApiService, liga);
        }

        public override async Task LoadAsync()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;

                if (Ligas.Count == 0)
                {
                    var ligas = await _futebolApiService.GetLigasAsync();

                    if (ligas != null)
                    {
                        foreach (var liga in ligas)
                        {
                            Ligas.Add(liga);
                        }
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
