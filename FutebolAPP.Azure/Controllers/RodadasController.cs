using FutebolAPP.Azure.Models;
using Microsoft.Azure.Mobile.Server.Config;
using Newtonsoft.Json;
using System.Web.Http;

namespace FutebolAPP.Azure.Controllers
{
    [MobileAppController]
    public class RodadasController : ApiController
    {
        // GET api/rodadas
        public IHttpActionResult Get(int id)
        {
            string jsonData = AzureUtils.GetBlobText(id, "fixtures");

            Rodadas rodadas = JsonConvert.DeserializeObject<Rodadas>(jsonData);

            return Ok(rodadas);
        }       
    }
}
