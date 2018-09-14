using System.Collections.Generic;

namespace FutebolAPP.Load.Model.FootballDataAPI
{
    public class LeagueTableSelf
    {
        public string href { get; set; }
    }

    public class LeagueTableCompetition
    {
        public string href { get; set; }
    }

    public class LeagueTableLinks
    {
        public LeagueTableSelf self { get; set; }
        public LeagueTableCompetition competition { get; set; }
    }

    public class LeagueTableTeam
    {
        public string href { get; set; }
    }

    public class LeagueTableLinks2
    {
        public LeagueTableTeam team { get; set; }
    }

    public class LeagueTableHome
    {
        public int goals { get; set; }
        public int goalsAgainst { get; set; }
        public int wins { get; set; }
        public int draws { get; set; }
        public int losses { get; set; }
    }

    public class LeagueTableAway
    {
        public int goals { get; set; }
        public int goalsAgainst { get; set; }
        public int wins { get; set; }
        public int draws { get; set; }
        public int losses { get; set; }
    }

    public class LeagueTableStanding
    {
        public LeagueTableLinks2 _links { get; set; }
        public int position { get; set; }
        public string teamName { get; set; }
        public string crestURI { get; set; }
        public int playedGames { get; set; }
        public int points { get; set; }
        public int goals { get; set; }
        public int goalsAgainst { get; set; }
        public int goalDifference { get; set; }
        public int wins { get; set; }
        public int draws { get; set; }
        public int losses { get; set; }
        public LeagueTableHome home { get; set; }
        public LeagueTableAway away { get; set; }
    }

    public class LeagueTable
    {
        public LeagueTableLinks _links { get; set; }
        public string leagueCaption { get; set; }
        public int matchday { get; set; }
        public List<LeagueTableStanding> standing { get; set; }
    }
}
