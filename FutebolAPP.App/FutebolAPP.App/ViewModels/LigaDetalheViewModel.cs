using System.Threading.Tasks;
using FutebolAPP.App.Models;
using FutebolAPP.App.Services;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;

namespace FutebolAPP.App.ViewModels
{
    public class LigaDetalheViewModel : BaseViewModel
    {
        private IFutebolApiService _futebolApiService;
        private Liga _liga;
        private Rodadas _rodadas;
        public ObservableCollection<Fixture> JogosRodadaAtual { get; }
        public ObservableCollection<LeagueTableStanding> Classificacao { get; }

        private int _rodadaAtual = 0;
        public int RodadaAtual
        {
            get { return _rodadaAtual; }
            set
            {
                if (SetProperty<int>(ref _rodadaAtual, value))
                {
                    ShowProximaRodadaCommand.ChangeCanExecute();
                    ShowRodadaAnteriorCommand.ChangeCanExecute();
                }
            }
        }
        public Command ShowProximaRodadaCommand { get; }
        public Command ShowRodadaAnteriorCommand { get; }

        public LigaDetalheViewModel(IFutebolApiService futebolApiService, Liga liga)
        {
            _futebolApiService = futebolApiService;
            _liga = liga;

            if (_liga.CurrentMatchday == 0)
                _liga.CurrentMatchday = 1;

            Title = _liga.Caption;
            JogosRodadaAtual = new ObservableCollection<Fixture>();
            Classificacao = new ObservableCollection<LeagueTableStanding>();
            ShowProximaRodadaCommand = new Command(ExecuteProximaRodadaCommand, CanExecuteProximaRodadaCommand);
            ShowRodadaAnteriorCommand = new Command(ExecuteRodadaAnteriorCommand, CanExecuteRodadaAnteriorCommand);

            RodadaAtual = _liga.CurrentMatchday;
        }

        public override async Task LoadAsync()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;

                var classificacao = await _futebolApiService.GetClassificacaoAsync(_liga.ID);
                _rodadas = await _futebolApiService.GetRodadasAsync(_liga.ID);

                Classificacao.Clear();

                if (classificacao != null)
                {
                    foreach (var position in classificacao.standing)
                    {
                        Classificacao.Add(position);
                    }
                }

                LoadRodada(_liga.CurrentMatchday);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void LoadRodada(int rodadaId)
        {
            JogosRodadaAtual.Clear();

            if (_rodadas != null)
            {
                List<Fixture> jogosRodadaAtual = _rodadas.fixtures.Where(f => f.matchday == rodadaId).ToList();

                if (jogosRodadaAtual != null)
                {
                    foreach (var jogo in jogosRodadaAtual)
                    {
                        JogosRodadaAtual.Add(jogo);
                    }
                }
            }
        }

        private void ExecuteProximaRodadaCommand()
        {
            RodadaAtual = RodadaAtual + 1;
            LoadRodada(RodadaAtual);
        }

        private bool CanExecuteProximaRodadaCommand()
        {
            return RodadaAtual < _liga.NumberOfMatchdays;
        }

        private void ExecuteRodadaAnteriorCommand()
        {
            RodadaAtual = RodadaAtual - 1;
            LoadRodada(RodadaAtual);
        }

        private bool CanExecuteRodadaAnteriorCommand()
        {
            return RodadaAtual > 1;
        }
    }
}
