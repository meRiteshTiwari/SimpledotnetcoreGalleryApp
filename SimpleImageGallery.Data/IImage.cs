using SimpleImageGallery.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleImageGallery.Data
{
    public interface IImage
    {
        IEnumerable<GalleryImage> GetAll();
        IEnumerable<GalleryImage> GetWithTag(string tag);
        Task<GalleryImage> GetById(int id);
    }
}
