using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EventEaseBookingSystem.Services
{
    public class BlobStorageService
    {
        private readonly string _connectionString;
        private readonly string _containerName;

        public BlobStorageService(IConfiguration configuration)
        {
            // Ensure configuration values are not null
            _connectionString = configuration["DefaultEndpointsProtocol=https;AccountName=eventeasedbst10275164;AccountKey=CZc/d5hv3DN0hRO4hsJqHpH7yNB1XhqP3PtSD9tNU3CVUb9XewY/vQO1c4PxgZjVrurpY6AQNkJ3+AStbrghQg==;EndpointSuffix=core.windows.net"];

            _containerName = configuration["images"];
                          
        }

        public async Task<string> UploadFileAsync(IFormFile file, string v)
        {
            // Create a unique file name
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            // Get blob container
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(_containerName);

            // Create container if it doesn't exist
            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            // Upload file
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            using (var stream = file.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(stream);
            }

            return blockBlob.Uri.ToString();
        }

        public async Task DeleteFileAsync(string blobUrl)
        {
            // Extract the blob name from the URL
            Uri uri = new Uri(blobUrl);
            string blobName = Path.GetFileName(uri.LocalPath);

            // Get blob reference
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(_containerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

            // Delete the blob
            await blockBlob.DeleteIfExistsAsync();
        }
    }
}