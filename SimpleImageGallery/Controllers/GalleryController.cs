using Microsoft.AspNetCore.Mvc;
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
        
        public IActionResult Index()
        {
            var hikingImageTags = new List<ImageTag>();
            var cityImageTags = new List<ImageTag>();

            var tag1 = new ImageTag()
            {
                Description = "Adventure",
                Id = 0
            };

            var tag2 = new ImageTag()
            {
                Description = "Urban",
                Id = 1
            };

            var tag3 = new ImageTag()
            {
                Description = "New York",
                Id = 2
            };

            hikingImageTags.Add(tag1);
            cityImageTags.AddRange(new List<ImageTag>{ tag2,tag3});

            var imageList = new List<GalleryImage>()
            {
                new GalleryImage()
                {
                    Title = "Hiking Trip",
                    Url= "https://images.pexels.com/photos/10325555/pexels-photo-10325555.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940",
                    Created = DateTime.Now,
                    Tags = hikingImageTags
                },
                new GalleryImage()
                {
                    Title = "On The Trail",
                    Url= "https://images.pexels.com/photos/10298360/pexels-photo-10298360.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940",
                    Created = DateTime.Now,
                    Tags = hikingImageTags
                },
                new GalleryImage()
                {
                    Title = "Downtown",
                    Url= "https://images.pexels.com/photos/9089028/pexels-photo-9089028.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940",
                    Created = DateTime.Now,
                    Tags = cityImageTags
                }
            };
            var model = new GalleryIndexModel()
            {
                Images = imageList,
                SearchQuery = ""
            };
            return View(model);
        }
    }
}
