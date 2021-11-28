using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using SimpleImageGallery.Data;
using SimpleImageGallery.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleImageGallery.Services
{
    public class ImageService : IImage
    {
        private readonly SimpleImageGalleryDbContext _ctx;
        public ImageService(SimpleImageGalleryDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task DeleteBlobContainer(int id)
        {
            var blobContainer = await _ctx.blobContainers.FirstOrDefaultAsync(c => c.Id == id);
            _ctx.blobContainers.Remove(blobContainer);
            _ctx.GalleryImages.RemoveRange(_ctx.GalleryImages.Where(c => c.BlobContainrId == id));
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteImage(int id)
        {
            var image = await _ctx.GalleryImages.FirstOrDefaultAsync(i=>i.Id==id);
            _ctx.GalleryImages.Remove(image);
            await _ctx.SaveChangesAsync();
        }

        public IEnumerable<GalleryImage> GetAll()
        {
            return _ctx.GalleryImages.Include(img => img.Tags);
        }

        public async Task<IEnumerable<BlobContainer>> GetAllBlob()
        {
            return await _ctx.blobContainers.ToListAsync();
        }

        public CloudBlobContainer GetBlobContainer(string azureConnectionString, string containerNamer)
        {
            var storageAccount = CloudStorageAccount.Parse(azureConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference(containerNamer);
        }

        public string GetBlobContainerIdbyImageId(int id)
        {
            return _ctx.GalleryImages.FirstOrDefault(i => i.Id == id).BlobContainrId.ToString();
        }

        public async Task<List<SelectListItem>> GetBlobList()
        {
            var list = new List<SelectListItem>();
            var listFromDb = await _ctx.blobContainers.ToListAsync();
            foreach(var item in listFromDb)
            {
                var temp = new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                };
                list.Add(temp);
            }
            return list;
        }

        public GalleryImage GetById(int id)
        {
            return GetAll().Where(img=>img.Id==id).First();
        }

        public string GetContainerName(string blobContainer)
        {
            var blob = _ctx.blobContainers.Find(Convert.ToInt32(blobContainer));
            if (blob == null)
            {
                return null;
            }
            return blob.Name;
            
        }

        public string GetFileNameById(int id)
        {
            return _ctx.GalleryImages.FirstOrDefault(i => i.Id == id).Url.Split('/')[4];
        }

        public IEnumerable<GalleryImage> GetWithTag(string tag)
        {
            return GetAll().Where(img => img.Tags.Any(t => t.Description == tag));
        }

        public async Task SetImage(string title, string tags, Uri uri, string blobid)
        {
            var image = new GalleryImage
            {
                Title = title,
                Tags = ParseTags(tags),
                Url = uri.AbsoluteUri,
                Created = DateTime.Now,
                BlobContainrId = Convert.ToInt32(blobid)
            };
            

            _ctx.Add(image);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateBlobDataBase(string containerName)
        {
            var blob = new BlobContainer();
            blob.Name = containerName;
            await _ctx.blobContainers.AddAsync(blob);
            await _ctx.SaveChangesAsync();
        }

        private List<ImageTag> ParseTags(string tags)
        {
            return tags.Split(",").Select(tag => new ImageTag { Description = tag }).ToList();
        }
    }
}
