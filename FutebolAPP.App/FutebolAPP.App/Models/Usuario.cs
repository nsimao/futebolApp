using Newtonsoft.Json;

namespace FutebolAPP.App.Models
{
    public class Usuario
    {
        [JsonProperty("userid")]
        public string UserId { get; set; }
    }
}
