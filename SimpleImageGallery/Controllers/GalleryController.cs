using Microsoft.AspNetCore.Mvc;
using SimpleImageGallery.Data;
using SimpleImageGallery.Data.Models;
using SimpleImageGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleImageGallery.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IImage _imageService;
        public GalleryController(IImage imageService)
        {
            _imageService = imageService;
        }
        
        public IActionResult Index()
        {
            var imageList = _imageService.GetAll();
            var model = new GalleryIndexModel()
            {
                Images = imageList,
                SearchQuery = ""
            };
            if (model.Images == null)
            {
                model.Images = new List<GalleryImage>();
            }
            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var image = _imageService.GetById(id);
            var model = new GalleryDetailModel()
            {
                Id = image.Id,
                Title = image.Title,
                CreatedOn = image.Created,
                Url = image.Url,
                Tags = image.Tags.Select(t => t.Description).ToList()
            };
            return View(model);
        }
    }
}
