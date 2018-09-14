using FutebolAPP.App.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutebolAPP.App.Services
{
    public interface IFutebolApiService
    {
        Task<List<Liga>> GetLigasAsync();
        Task<Classificacao> GetClassificacaoAsync(int ligaId);
        Task<Rodadas> GetRodadasAsync(int ligaId);
        Task<bool> LoginAsync();
        Task<bool> LogoutAsync();
    }
}
