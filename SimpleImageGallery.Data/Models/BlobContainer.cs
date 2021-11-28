using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleImageGallery.Data.Models
{
    public class BlobContainer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<GalleryImage> Images { get; set; }
    }
}
