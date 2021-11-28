using Microsoft.AspNetCore.Mvc.Rendering;
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
        Task SetImage(string title, string tags, Uri uri,string blobContainerid);
        Task<List<SelectListItem>> GetBlobList();
        Task UpdateBlobDataBase(string containerName);
        string GetContainerName(string blobContainer);
        string GetFileNameById(int id);
        string GetBlobContainerIdbyImageId(int id);
        Task DeleteImage(int id);
        Task<IEnumerable<BlobContainer>> GetAllBlob();
        Task DeleteBlobContainer(int id);
    }
}
