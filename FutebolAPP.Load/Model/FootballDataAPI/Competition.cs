namespace FutebolAPP.Load.Model.FootballDataAPI
{
    public class CompetitionSelf
    {
        public string href { get; set; }
    }

    public class CompetitionTeams
    {
        public string href { get; set; }
    }

    public class CompetitionFixtures
    {
        public string href { get; set; }
    }

    public class CompetitionLeagueTable
    {
        public string href { get; set; }
    }

    public class CompetitionLinks
    {
        public CompetitionSelf self { get; set; }
        public CompetitionTeams teams { get; set; }
        public CompetitionFixtures fixtures { get; set; }
        public CompetitionLeagueTable leagueTable { get; set; }
    }

    public class Competition
    {
        public CompetitionLinks _links { get; set; }
        public int id { get; set; }
        public string caption { get; set; }
        public string league { get; set; }
        public string year { get; set; }
        public int currentMatchday { get; set; }
        public int numberOfMatchdays { get; set; }
        public int numberOfTeams { get; set; }
        public int numberOfGames { get; set; }
        public string lastUpdated { get; set; }
    }
}
