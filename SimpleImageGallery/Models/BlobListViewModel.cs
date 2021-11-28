using SimpleImageGallery.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleImageGallery.Models
{
    public class BlobListViewModel
    {
        public IEnumerable<BlobContainer> blobContainers { get; set; }
    }
}
