using System.Collections.Generic;

namespace FutebolAPP.Azure.Models
{
    public class FixtureSelf
    {
        public string href { get; set; }
    }

    public class FixtureCompetition
    {
        public string href { get; set; }
    }

    public class FixtureLinks
    {
        public FixtureSelf self { get; set; }
        public FixtureCompetition competition { get; set; }
    }

    public class FixtureSelf2
    {
        public string href { get; set; }
    }

    public class FixtureCompetition2
    {
        public string href { get; set; }
    }

    public class FixtureHomeTeam
    {
        public string href { get; set; }
    }

    public class FixtureAwayTeam
    {
        public string href { get; set; }
    }

    public class FixtureLinks2
    {
        public FixtureSelf2 self { get; set; }
        public FixtureCompetition2 competition { get; set; }
        public FixtureHomeTeam homeTeam { get; set; }
        public FixtureAwayTeam awayTeam { get; set; }
    }

    public class FixtureHalfTime
    {
        public int goalsHomeTeam { get; set; }
        public int goalsAwayTeam { get; set; }
    }

    public class FixtureResult
    {
        public int? goalsHomeTeam { get; set; }
        public int? goalsAwayTeam { get; set; }
        public FixtureHalfTime halfTime { get; set; }
    }

    public class FixtureOdds
    {
        public double homeWin { get; set; }
        public double draw { get; set; }
        public double awayWin { get; set; }
    }

    public class Fixture
    {
        public FixtureLinks2 _links { get; set; }
        public string date { get; set; }
        public string status { get; set; }
        public int matchday { get; set; }
        public string homeTeamName { get; set; }
        public string awayTeamName { get; set; }
        public FixtureResult result { get; set; }
        public FixtureOdds odds { get; set; }

        public string GetFullDescription
        {
            get
            {
                return $"{homeTeamName} {result.goalsHomeTeam} X {result.goalsAwayTeam} {awayTeamName}";
            }
        }
    }

    public class Rodadas
    {
        public FixtureLinks _links { get; set; }
        public int count { get; set; }
        public List<Fixture> fixtures { get; set; }
    }
}