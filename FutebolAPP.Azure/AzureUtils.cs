using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace FutebolAPP.Azure
{
    public class AzureUtils
    {
        public static string GetBlobText(int ligaId, string blobContainer)
        {
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(blobContainer);

            // Retrieve reference to a blob by name
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(ligaId.ToString());

            return blockBlob.DownloadText();
        }
    }
}