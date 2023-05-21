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

namespace BussinessLogic.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        } 

        public Task DeleteBlobAsync(string blobname)
        {
            throw new NotImplementedException();
        }

      
        public async Task<BlobInfo> GetBlobAsync(string name)
        {
            throw new NotImplementedException();
        }   

        public async Task<IEnumerable<string>> ListBlobsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UploadContentBlobAsync(IFormFile file, string fileName, string userName)
        {
            //create blob container if does not exist
            var containerInstance = _blobServiceClient.GetBlobContainerClient("timecapsule");
            var blobName = $"{userName}/{fileName}";

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
    }
}
