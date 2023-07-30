using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BussinessLogic.Services
{
    public class BlobService : IBlobService
    {
        private string _localFolder = "TCContentData";
        private readonly IConfiguration _config;

        public BlobService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<string>> ListBlobsAsync(string userName, string timeCapsule)
        {
            var folderPath = Path.Combine(_localFolder, userName, timeCapsule);

            // Modify the folderPath to create a relative path
            var relativeFolderPath = Path.Combine(GetSolutionRootDirectory(), folderPath);

            if (!Directory.Exists(relativeFolderPath))
            {
                return Enumerable.Empty<string>();
            }
            DirectoryInfo di = new DirectoryInfo(relativeFolderPath);

            var fileUrls = di.GetFiles()
                .Select(file => file.FullName).ToList();

            return fileUrls;
        }

        static string GetSolutionRootDirectory()
        {
            // You can find the solution root directory by traversing up from the current working directory
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Navigate upwards until you find a folder containing the ".sln" file (solution file)
            while (currentDirectory != null)
            {
                if (Directory.GetFiles(currentDirectory, "*.sln").Length > 0)
                {
                    return currentDirectory;
                }
                currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
            }

            // If the solution root directory is not found, you can handle this case accordingly
            throw new Exception("Solution root directory not found.");
        }

        private string GetLocalFileUrl(string filePath)
        {
            // Assuming you want to generate a URL that can be accessed locally through a web server.
            // You can construct any URL you want to access the file locally.
            // For example, if you're using ASP.NET Core, you can construct a URL like this:
            string baseUrl = _config["BaseUrl"]; // e.g., "http://localhost:5000"
            string relativePath = filePath.Replace(_localFolder, string.Empty).TrimStart(Path.DirectorySeparatorChar);
            string localFileUrl = Path.Combine(baseUrl, "TCContentData", relativePath).Replace('\\', '/');

            return localFileUrl;
        }

        public async Task UploadContentBlobAsync(IFormFile file, string fileName, string timeCapsuleId, string userId)
        {
            try
            {
                string localFolder = Path.Combine(GetSolutionRootDirectory(), _localFolder);

                // Create the folder if it doesn't exist
                if (!Directory.Exists(localFolder))
                {
                    Directory.CreateDirectory(localFolder);
                }

                // Create the path to the file
                string filePath = Path.Combine(localFolder, userId, timeCapsuleId);
                Directory.CreateDirectory(filePath); // this will create all directories and subdirectories in the path

                filePath = Path.Combine(filePath, fileName);

                if (File.Exists(filePath))
                {
                    throw new InvalidOperationException("A file with the same name already exists in the local folder.");
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }




        public Task<BlobInfo> GetBlobAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteBlobAsync(string blobName)
        {
            throw new NotImplementedException();
        }
    }
}
