using FutebolAPP.Azure.Models;
using Microsoft.Azure.Mobile.Server.Config;
using Newtonsoft.Json;
using System.Web.Http;

namespace FutebolAPP.Azure.Controllers
{
    [MobileAppController]
    public class ClassificacaoController : ApiController
    {
        // GET api/classificacao
        public IHttpActionResult Get(int id)
        {
            string jsonData = AzureUtils.GetBlobText(id, "leaguetable");

            Classificacao classificacao = JsonConvert.DeserializeObject<Classificacao>(jsonData);

            return Ok(classificacao);
        }
    }
}
