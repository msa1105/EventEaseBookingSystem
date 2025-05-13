namespace EventEaseBookingSystem.Services;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class BlobService
{
    private readonly BlobContainerClient _containerClient;

    public BlobService(IConfiguration configuration)
    {
        var connectionString = configuration["AzureBlobStorage:ConnectionString"];
        var containerName = configuration["AzureBlobStorage:ContainerName"];
        var blobServiceClient = new BlobServiceClient(connectionString);
        _containerClient = blobServiceClient.GetBlobContainerClient(containerName);
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        var blobClient = _containerClient.GetBlobClient(Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
        using var stream = file.OpenReadStream();
        await blobClient.UploadAsync(stream);
        return blobClient.Uri.ToString();
    }
}
