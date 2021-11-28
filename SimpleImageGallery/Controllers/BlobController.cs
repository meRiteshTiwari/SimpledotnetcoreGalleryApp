using Azure;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SimpleImageGallery.Data;
using SimpleImageGallery.Data.Models;
using SimpleImageGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleImageGallery.Controllers
{
    public class BlobController : Controller
    {
        public IImage _ImageService { get; }
        private readonly IConfiguration _config;
        public BlobController(IImage imageService,IConfiguration config)
        {
            _ImageService = imageService;
            _config = config;
        }

        

        public IActionResult CreateBlob()
        {
            var model = new CreateBlobModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewBlob(string name)
        {
            string containerName = name;
            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(_config["AzureStorageConnectionString"]);
            try
            {
                // Create the container
                BlobContainerClient container = await blobServiceClient.CreateBlobContainerAsync(containerName,Azure.Storage.Blobs.Models.PublicAccessType.Blob);
                //container.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                if (await container.ExistsAsync())
                {
                    Console.WriteLine("Created container {0}", container.Name);
                    await _ImageService.UpdateBlobDataBase(containerName);
                    //return container;
                }
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine("HTTP error code {0}: {1}",
                                    e.Status, e.ErrorCode);
                Console.WriteLine(e.Message);
            }
            return RedirectToAction("Upload", "Image");
        }

        public async Task<IActionResult> GetAllBlob()
        {
            var blobList = await _ImageService.GetAllBlob();

            return View(new BlobListViewModel { blobContainers=blobList});
        }
        public async Task<IActionResult> DeleteBlobContainer(int id)
        {
            var containerName = _ImageService.GetContainerName(id.ToString());
            BlobServiceClient blobServiceClient = new BlobServiceClient(_config["AzureStorageConnectionString"]);

            BlobContainerClient container = blobServiceClient.GetBlobContainerClient(containerName);

            try
            {
                // Delete the specified container and handle the exception.
                await container.DeleteAsync();
                await _ImageService.DeleteBlobContainer(id);
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine("HTTP error code {0}: {1}",
                                    e.Status, e.ErrorCode);
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
            return RedirectToAction("Index", "Gallery");
        }
        
    }
}
