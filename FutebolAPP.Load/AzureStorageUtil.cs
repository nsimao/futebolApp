using FutebolAPP.Load.Model.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;
using System.Threading;

namespace FutebolAPP.Load
{
    public class AzureStorageUtil
    {
        private string StorageConnectionString;
        private CloudStorageAccount StorageAccount;

        public AzureStorageUtil()
        {
            StorageConnectionString = ConfigurationManager.AppSettings["StorageAzure"];
        }

        public void ClearAzureStructure()
        {
            // Retrieve the storage account from the connection string.
            StorageAccount = CloudStorageAccount.Parse(StorageConnectionString);

            // Tabela Competitions
            CloudTableClient tableClient = StorageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("competition");
            table.DeleteIfExists();

            // Blob Container LeagueTable
            CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();
            CloudBlobContainer containerLeagueTable = blobClient.GetContainerReference("leaguetable");
            containerLeagueTable.DeleteIfExists();

            CloudBlobContainer containerFixtures = blobClient.GetContainerReference("fixtures");
            containerFixtures.DeleteIfExists();

            Thread.Sleep(10000);
        }

        public void AddCompetition(Competition competition)
        {
            // Retrieve the storage account from the connection string.
            StorageAccount = CloudStorageAccount.Parse(StorageConnectionString);

            // Tabela Competitions
            CloudTableClient tableClient = StorageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("competition");
            table.CreateIfNotExists();

            TableOperation insertOperation = TableOperation.Insert(competition);
            table.Execute(insertOperation);
        }

        public void AddLeagueTableData(Competition competition, string jsonData)
        {
            // Retrieve the storage account from the connection string.
            StorageAccount = CloudStorageAccount.Parse(StorageConnectionString);

            // Blob Container LeagueTable
            CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("leaguetable");
            container.CreateIfNotExists();

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(competition.id.ToString());

            blockBlob.UploadText(jsonData);
        }

        public void AddFixturesData(Competition competition, string jsonData)
        {
            // Retrieve the storage account from the connection string.
            StorageAccount = CloudStorageAccount.Parse(StorageConnectionString);

            // Blob Container Fixtures
            CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("fixtures");
            container.CreateIfNotExists();
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(competition.id.ToString());
            blockBlob.UploadText(jsonData);
        }
    }
}
