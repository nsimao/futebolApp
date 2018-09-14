using FutebolAPP.Load.Model.FootballDataAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FutebolAPP.Load
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            string jsonData = GetJsonData(@"http://www.football-data.org/v1/competitions").Result;
            List<Competition> competitions = JsonConvert.DeserializeObject<List<Competition>>(jsonData);

            ProcessarCompetitions(competitions);

            MessageBox.Show("Finalizado!", "Azure", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void ProcessarCompetitions(List<Competition> competitions)
        {
            AzureStorageUtil azureStorageUtil = new AzureStorageUtil();

            // Elimina Tudo;
            azureStorageUtil.ClearAzureStructure();

            List<string> ignoredCompetitions = GetIgnoredCompetitions();
            List<dynamic> competitionsProcessed = new List<dynamic>();

            foreach (Competition competition in competitions)
            {

                dynamic competitionProcessed = new ExpandoObject();
                competitionProcessed.Name = competition.caption;
                competitionProcessed.Id = competition.id;
                competitionsProcessed.Add(competitionProcessed);


                if (ignoredCompetitions.Count(s => s.Equals(competition.league)) == 0)
                {
                    LeagueTable leagueTable = GetLeagueTable(competition.id);
                    Fixtures fixtures = GetFixtures(competition.id);

                    // Somente campeonatos pontos corridos
                    if ((leagueTable != null) && (leagueTable.standing != null) && (fixtures != null))
                    {
                        string jsonLeagueTable = JsonConvert.SerializeObject(leagueTable);
                        string jsonFixtures = JsonConvert.SerializeObject(fixtures);

                        Model.Azure.Competition competitionAzure = GetCompetitionAzure(competition);

                        azureStorageUtil.AddCompetition(competitionAzure);
                        azureStorageUtil.AddLeagueTableData(competitionAzure, jsonLeagueTable);
                        azureStorageUtil.AddFixturesData(competitionAzure, jsonFixtures);

                        competitionProcessed.Status = "OK";
                    }
                    else
                    {
                        competitionProcessed.Status = "Does not have LeagueTable.";
                    }
                }
                else
                {
                    competitionProcessed.Status = "Ignorada";
                }
            }

            dataGridView1.DataSource = competitionsProcessed.ToDataTable();
            dataGridView1.Refresh();
        }

        private Model.Azure.Competition GetCompetitionAzure(Competition competition)
        {
            Model.Azure.Competition competitionAzure = new Model.Azure.Competition(competition.id);

            competitionAzure.caption = competition.caption;
            competitionAzure.league = competition.league;
            competitionAzure.year = competition.year;
            competitionAzure.currentMatchday = competition.currentMatchday;
            competitionAzure.numberOfMatchdays = competition.numberOfMatchdays;
            competitionAzure.numberOfTeams = competition.numberOfTeams;
            competitionAzure.numberOfGames = competition.numberOfGames;
            competitionAzure.lastUpdated = competition.lastUpdated;

            return competitionAzure;
        }
        private LeagueTable GetLeagueTable(int competitionId)
        {
            try
            {
                string jsonData = GetJsonData($"http://www.football-data.org/v1/competitions/{competitionId}/leagueTable").Result;

                jsonData = jsonData.RemoveAccents();
                LeagueTable leagueTable = JsonConvert.DeserializeObject<LeagueTable>(jsonData);
                return leagueTable;
            }
            catch
            {
                return null;
            }
        }

        private Fixtures GetFixtures(int competitionId)
        {
            try
            {
                string jsonData = GetJsonData($"http://api.football-data.org/v1/competitions/{competitionId}/fixtures").Result;

                jsonData = jsonData.RemoveAccents();
                Fixtures fixtures = JsonConvert.DeserializeObject<Fixtures>(jsonData);

                return fixtures;
            }
            catch
            {
                return null;
            }
        }

        private async Task<string> GetJsonData(string url)
        {

            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("X-Auth-Token", ConfigurationManager.AppSettings["TokenAPI"]);

                var response = await httpClient.GetAsync(url).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        return await new StreamReader(responseStream)
                                    .ReadToEndAsync().ConfigureAwait(false);
                    }
                }
            }
            catch
            {

            }
            return null;
        }

        private List<string> GetIgnoredCompetitions()
        {
            string ignoredCompetitions = ConfigurationManager.AppSettings["IgnoredCompetitions"];

            if (!string.IsNullOrWhiteSpace(ignoredCompetitions))
            {
                return ignoredCompetitions.Split(';').ToList();
            }

            return new List<string>();
        }
    }
}