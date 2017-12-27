using Microsoft.EntityFrameworkCore;
using ShopWebsite.DAL.Context;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.ImageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Implementations
{
    public class ImageRepository : IImageRepository
    {
        private ShopWebsiteSqlContext _context;

        public ImageRepository(ShopWebsiteSqlContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(ImageModel imageModel)
        {
            var images = await _context.ImageModels.Where(image => image.Id == imageModel.Id).ToListAsync();
            if (images == null || images.Count == 0)
            {
                _context.Add(imageModel);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<ImageModel>> GetAll()
        {
            var images = await _context.ImageModels.AsNoTracking().ToListAsync();
            if (images == null || images.Count == 0)
            {
                return null;
            }
            else
            {
                return images;
            }
        }

        public async Task<ImageModel> GetBy(string id)
        {
            var images = await _context.ImageModels.Where(image => image.Id == id).ToListAsync();
            if (images == null || images.Count == 0)
            {
                return null;
            }
            else
            {
                var image = images[0];

                return image;
            }
        }

        public async Task<bool> Remove(string id)
        {
            var images = await _context.ImageModels.Where(image => image.Id == id).ToListAsync();
            if (images == null || images.Count == 0)
            {
                return false;
            }
            else
            {
                var image = images[0];

                _context.Remove(image);
                _context.SaveChanges();

                return true;
            }
        }
    }
}
