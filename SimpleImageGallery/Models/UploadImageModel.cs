using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleImageGallery.Models
{
    public class UploadImageModel
    {
        public string Title { get; set; }
        public string Tags { get; set; }
        public string BlobContainer { get; set; }
        public List<SelectListItem> BlobContainers { get; set; }
        public IFormFile ImageUpload { get; set; }
    }
}
