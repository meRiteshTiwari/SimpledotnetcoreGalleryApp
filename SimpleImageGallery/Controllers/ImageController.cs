using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using SimpleImageGallery.Data;
using SimpleImageGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SimpleImageGallery.Controllers
{
    public class ImageController : Controller
    {
        private IConfiguration _config;
        private readonly IImage _imageservice;

        private string AzureConnectionString { get; }
        public ImageController(IConfiguration config,IImage imageservice)
        {
            _config = config;
            _imageservice = imageservice;
            AzureConnectionString = _config["AzureStorageConnectionString"];
        }
        public async Task<IActionResult> Upload()
        {
            var model = new UploadImageModel();
            model.BlobContainers = await _imageservice.GetBlobList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadNewImage(IFormFile file, string tags, string title, UploadImageModel model)
        {
            var containerName = _imageservice.GetContainerName(model.BlobContainer);
            var container = _imageservice.GetBlobContainer(AzureConnectionString, containerName);
            var content = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
            var fileName = content.FileName.Trim('"');

            //Get a reference to a Block Blob
            var blockBlob = container.GetBlockBlobReference(fileName);
            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());
            await _imageservice.SetImage(title, tags, blockBlob.Uri,model.BlobContainer);

            return RedirectToAction("Index", "Gallery");

        }

        public async Task<IActionResult> deleteImage(int id)
        {
            //have to use id to get blobcontainer id from galleryImage table and then have to call below method.
            var blobContainerId = _imageservice.GetBlobContainerIdbyImageId(id);
            var containerName = _imageservice.GetContainerName(blobContainerId);
            var container = _imageservice.GetBlobContainer(AzureConnectionString, containerName);
            string fileName = _imageservice.GetFileNameById(id);
            var blockBlob = container.GetBlockBlobReference(fileName);
            await blockBlob.DeleteIfExistsAsync();
            await _imageservice.DeleteImage(id);
            return RedirectToAction("Index", "Gallery");
        }
    }
}
