using Microsoft.WindowsAzure.Storage.Table;

namespace FutebolAPP.Azure.Models
{
    public class Competition : TableEntity
    {
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