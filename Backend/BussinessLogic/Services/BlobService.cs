using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Azure.Storage.Sas;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace BussinessLogic.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _config;


        public BlobService(BlobServiceClient blobServiceClient, IConfiguration config)
        {
            _blobServiceClient = blobServiceClient;
            _config = config;
        }

        public Task DeleteBlobAsync(string blobname)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<string>> ListBlobsAsync(string userName, string timeCapsule)
        {
            var containerInstance = _blobServiceClient.GetBlobContainerClient("tc1container");
            var blobs = new List<string>();
            var folderPrefix = $"{userName}/{timeCapsule}/";

            await foreach (var blobItem in containerInstance.GetBlobsByHierarchyAsync(prefix: folderPrefix, delimiter: "/"))
            {
                if (!blobItem.IsPrefix)
                {
                    // Construct the URL for each blob with the SAS token.
                    string blobUrl = GetBlobSasUrl("tc1container", blobItem.Blob.Name);

                    blobs.Add(blobUrl);
                }
            }
            return blobs;
        }

        public async Task<BlobInfo>  GetBlobAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UploadContentBlobAsync(IFormFile file, string fileName,string timeCapsuleId, string userId)
        {
            //create blob container if does not exist
            var containerInstance = _blobServiceClient.GetBlobContainerClient("tc1container");
            var blobName = $"{userId}/{timeCapsuleId}/{fileName}";

            var blobInstance = containerInstance.GetBlobClient(blobName);

            if (await blobInstance.ExistsAsync())
            {
                throw new InvalidOperationException("Blob with the same name already exists.");
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                await blobInstance.UploadAsync(memoryStream);
            }
        }

        public string GenerateSasTokenForContainer(string accountName, string accountKey, string containerName, BlobContainerSasPermissions permissions, TimeSpan validityPeriod)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_config.GetConnectionString("AzureBlobStorage"));
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            BlobSasBuilder sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = containerName,
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.Add(validityPeriod)
            };
            sasBuilder.SetPermissions(permissions);  
            StorageSharedKeyCredential sharedKeyCredential = new StorageSharedKeyCredential(accountName, accountKey);
            string sasToken = sasBuilder.ToSasQueryParameters(sharedKeyCredential).ToString();
            return sasToken;
        }

        public string GetBlobSasUrl(string containerName,  string pathToFile)
        {

            var match = Regex.Match(_config.GetConnectionString("AzureBlobStorage"), "AccountName=(?<accountName>[^;]+);AccountKey=(?<accountKey>[^;]+)");
            string accountName = match.Groups["accountName"].Value;
            string accountKey = match.Groups["accountKey"].Value;

            string sasToken = GenerateSasTokenForContainer(accountName, accountKey, containerName, BlobContainerSasPermissions.Read, TimeSpan.FromMinutes(5));

            return $"https://{accountName}.blob.core.windows.net/{containerName}/{pathToFile}?{sasToken}";
        }
    }
}
