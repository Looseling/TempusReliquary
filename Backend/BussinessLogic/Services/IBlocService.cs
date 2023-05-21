using Azure.Storage.Blobs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BussinessLogic.Services
{
    public interface IBlobService
    {
        public Task<BlobInfo> GetBlobAsync(string name);
        public Task<IEnumerable<string>> ListBlobsAsync();
        public Task UploadContentBlobAsync(IFormFile file, string filename,string userName);
        public Task DeleteBlobAsync(string blobName);


    }   
}