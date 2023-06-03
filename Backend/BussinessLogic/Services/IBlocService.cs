using Azure.Storage.Blobs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BussinessLogic.Services
{
    public interface IBlobService
    {
        public Task<BlobInfo> GetBlobAsync();
        public Task<IEnumerable<string>> ListBlobsAsync(string name, string timeCapsulse);
        public Task UploadContentBlobAsync(IFormFile file, string filename,string userName, string timeCapsule);
        public Task DeleteBlobAsync(string blobName);
    }   
}