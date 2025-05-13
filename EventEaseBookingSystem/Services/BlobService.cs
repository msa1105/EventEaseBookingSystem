namespace EventEaseBookingSystem.Services;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

public class BlobService
{
    private readonly string _connectionString = "YourAzureBlobStorageConnectionString";
    private readonly string _containerName = "your-container-name";

    public async Task<string> UploadFileAsync(IFormFile file, string v)
    {
        var blobClient = new BlobContainerClient(_connectionString, _containerName);
        await blobClient.CreateIfNotExistsAsync();
        var blob = blobClient.GetBlobClient(file.FileName);
        using (var stream = file.OpenReadStream())
        {
            await blob.UploadAsync(stream, overwrite: true);
        }
        return blob.Uri.ToString();
    }
}
