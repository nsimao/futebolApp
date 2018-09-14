using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FutebolAPP.App.Models;
using Xamarin.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.MobileServices;
using FutebolAPP.App.Helpers;
using FutebolAPP.App.Authentication;

[assembly: Dependency(typeof(FutebolAPP.App.Services.FutebolApiService))]
namespace FutebolAPP.App.Services
{
    public class FutebolApiService : IFutebolApiService
    {
        private const string BaseUrl = "https://futebolapi.azurewebsites.net/api/";

        private MobileServiceClient mobileServiceClient = null;

        public void Initialize()
        {
            mobileServiceClient = new MobileServiceClient(BaseUrl);

            if (!string.IsNullOrWhiteSpace(Settings.AuthToken) && !string.IsNullOrWhiteSpace(Settings.UserId))
            {
                mobileServiceClient.CurrentUser = new MobileServiceUser(Settings.UserId);
                mobileServiceClient.CurrentUser.MobileServiceAuthenticationToken = Settings.AuthToken;
            }
        }

        private async Task<string> GetJsonData(string url)
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                var response = await httpClient.GetAsync(url).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        return await new StreamReader(responseStream)
                                    .ReadToEndAsync().ConfigureAwait(false);
                    }
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Liga>> GetLigasAsync()
        {
            try
            {
                string url = $"{BaseUrl}Ligas";
                string jsonData = await GetJsonData(url);

                return JsonConvert.DeserializeObject<List<Liga>>(jsonData);
            }
            catch
            {
                return null;
            }
        }

        public async Task<Classificacao> GetClassificacaoAsync(int ligaId)
        {
            try
            {
                string url = $"{BaseUrl}Classificacao/{ligaId}";
                string jsonData = await GetJsonData(url);

                return JsonConvert.DeserializeObject<Classificacao>(jsonData);
            }
            catch
            {
                return null;
            }
        }

        public async Task<Rodadas> GetRodadasAsync(int ligaId)
        {
            try
            {
                string url = $"{BaseUrl}Rodadas/{ligaId}";
                string jsonData = await GetJsonData(url);

                return JsonConvert.DeserializeObject<Rodadas>(jsonData);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> LoginAsync()
        {
            Initialize();

            var auth = DependencyService.Get<IAuthentication>();
            var user = await auth.LoginAsync(mobileServiceClient, MobileServiceAuthenticationProvider.Facebook);

            if (user != null)
            {
                Settings.AuthToken = user.MobileServiceAuthenticationToken;
                Settings.UserId = user.UserId;

                await GetFacebookData();

                return true;
            }
            else
            {
                Settings.AuthToken = string.Empty;
                Settings.UserId = string.Empty;
                Settings.UserFullName = string.Empty;
                Settings.UserPictureURL = string.Empty;

                return false;
            }
        }

        private async Task GetFacebookData()
        {
            List<AppServiceIdentity> appServiceidentities = await mobileServiceClient.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me");

            string fullName = appServiceidentities[0].UserClaims.Find(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")).Value;
            string facebookID = appServiceidentities[0].UserClaims.Find(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")).Value;

            //string userToken = appServiceidentities[0].AccessToken;
            //string requestUrl = $"https://graph.facebook.com/v2.9/me/?fields=picture&access_token={userToken}";
            //HttpClient httpClient = new HttpClient();
            //string userJson = await httpClient.GetStringAsync(requestUrl);
            //FacebookProfile facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);

            Settings.UserFullName = fullName;
            Settings.UserPictureURL = $"https://graph.facebook.com/{facebookID}/picture?type=large";
        }

        public async Task<bool> LogoutAsync()
        {
            Initialize();

            var auth = DependencyService.Get<IAuthentication>();
            bool result = await auth.LogoutAsync(mobileServiceClient, MobileServiceAuthenticationProvider.Facebook);

            if (result)
            {
                Settings.AuthToken = string.Empty;
                Settings.UserId = string.Empty;
                Settings.UserFullName = string.Empty;
                Settings.UserPictureURL = string.Empty;
            }

            return result;
        }

        #region Mock

        public async Task<List<Liga>> GetLigasMockAsync()
        {
            List<Liga> ligas = new List<Liga>();

            ligas.Add(new Liga() { ID = 1, Caption = "Espanhol", CurrentMatchday = 37, League = "ESP", NumberOfMatchdays = 38 });
            ligas.Add(new Liga() { ID = 1, Caption = "Italiano", CurrentMatchday = 36, League = "ITA", NumberOfMatchdays = 38 });

            return await Task.FromResult<List<Liga>>(ligas);
        }

        public async Task<Classificacao> GetClassificacaoMockAsync(int ligaID)
        {
            Classificacao classificacao = new Classificacao();

            classificacao.standing = new List<LeagueTableStanding>();

            classificacao.standing.Add(new LeagueTableStanding()
            {
                position = 1,
                teamName = "Real Madrid",
                playedGames = 36,
                points = 90,
                wins = 30,
                draws = 1,
                goals = 120,
                goalDifference = 30,
                crestURI = "http://upload.wikimedia.org/wikipedia/de/3/3f/Real_Madrid_Logo.svg"
            });

            classificacao.standing.Add(new LeagueTableStanding()
            {
                position = 2,
                teamName = "Barcelona",
                playedGames = 36,
                points = 80,
                wins = 20,
                draws = 5,
                goals = 100,
                goalDifference = 2,
                crestURI = "http://upload.wikimedia.org/wikipedia/de/a/aa/Fc_barcelona.svg"
            });

            classificacao.standing.Add(new LeagueTableStanding()
            {
                position = 3,
                teamName = "Sevilla",
                playedGames = 36,
                points = 79,
                wins = 19,
                draws = 10,
                goals = 89,
                goalDifference = 22,
                crestURI = "http://upload.wikimedia.org/wikipedia/en/8/86/Sevilla_cf_200px.png"
            });

            return await Task.FromResult<Classificacao>(classificacao);
        }

        public async Task<Rodadas> GetRodadasMockAsync(int ligaID)
        {
            Rodadas rodadas = new Rodadas();

            rodadas.fixtures = new List<Fixture>();

            rodadas.fixtures.Add(new Fixture()
            {
                awayTeamName = "Real Madrid",
                date = DateTime.Today.ToString(),
                homeTeamName = "Sevilla",
                result = new FixtureResult() { goalsAwayTeam = 3, goalsHomeTeam = 1 }
            });
            rodadas.fixtures.Add(new Fixture()
            {
                awayTeamName = "Real Madrid",
                date = DateTime.Today.ToString(),
                homeTeamName = "Barcelona",
                result = new FixtureResult() { goalsAwayTeam = 3, goalsHomeTeam = 2 }
            });

            return await Task.FromResult<Rodadas>(rodadas);

        }


        #endregion
    }
}
