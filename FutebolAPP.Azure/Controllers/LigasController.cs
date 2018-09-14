using FutebolAPP.Azure.Models;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;
using System.Web.Http;

namespace FutebolAPP.Azure.Controllers
{
    [MobileAppController]
    public class LigasController : ApiController
    {
        // GET api/ligas
        public IHttpActionResult Get()
        {
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "people" table.
            CloudTable table = tableClient.GetTableReference("competition");

            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<Competition> query = new TableQuery<Competition>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "competition"));

            var ligas = table.ExecuteQuery(query);

            return Ok(ligas);
        }
    }
}
