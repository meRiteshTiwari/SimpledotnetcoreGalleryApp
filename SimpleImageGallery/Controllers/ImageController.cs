using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Upload()
        {
            var model = new UploadImageModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadNewImage(IFormFile file, string tags, string title)
        {
            var container = _imageservice.GetBlobContainer(AzureConnectionString, "images");
            var content = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
            var fileName = content.FileName.Trim('"');

            //Get a reference to a Block Blob
            var blockBlob = container.GetBlockBlobReference(fileName);
            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());
            await _imageservice.SetImage(title, tags, blockBlob.Uri);

            return RedirectToAction("Index", "Gallery");

        }
    }
}
