namespace FutebolAPP.App.Models
{
    public class Liga
    {
        public int ID { get; set; }
        public string Caption { get; set; }
        public string League { get; set; }
        public string Year { get; set; }
        public int CurrentMatchday { get; set; }
        public int NumberOfMatchdays { get; set; }
        public int NumberOfTeams { get; set; }
        public int NumberOfGames { get; set; }
        public string LastUpdated { get; set; }
        public string FullMatchDayDescription
        {
            get
            {
                return $"Rodada {CurrentMatchday} / {NumberOfMatchdays}";
            }
        }
    }
}
