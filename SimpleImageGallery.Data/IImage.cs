using Microsoft.WindowsAzure.Storage.Blob;
using SimpleImageGallery.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleImageGallery.Data
{
    public interface IImage
    {
        IEnumerable<GalleryImage> GetAll();
        IEnumerable<GalleryImage> GetWithTag(string tag);
        GalleryImage GetById(int id);
        CloudBlobContainer GetBlobContainer(string azureConnectionString, string v);
        Task SetImage(string title, string tags, Uri uri);
    }
}
