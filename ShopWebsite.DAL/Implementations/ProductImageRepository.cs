using Microsoft.EntityFrameworkCore;
using ShopWebsite.DAL.Context;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Implementations
{
    public class ProductImageRepository : IProductImageRepository
    {
        private ShopWebsiteSqlContext _context;

        public ProductImageRepository(ShopWebsiteSqlContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(ProductImage newProductImage)
        {
            var productImageExist = await _context.ProductImages.Where(productImage => 
                (productImage.ImageModelId == newProductImage.ImageModelId && productImage.ProductId == newProductImage.ProductId))
                .Take(1).ToListAsync();
            if(productImageExist != null && productImageExist.Count > 0)
            {
                return false;
            }
            else
            {
                _context.Add(newProductImage);
                _context.SaveChanges();

                return true;
            }
        }

        public async Task<bool> Remove(string productId, string imageModelId)
        {
            var productImageExist = await _context.ProductImages.Where(productImage =>
                (productImage.ImageModelId == imageModelId && productImage.ProductId == productId))
                .Take(1).ToListAsync();
            if (productImageExist != null && productImageExist.Count > 0)
            {
                var searchProductImage = productImageExist[0];

                _context.Remove(searchProductImage);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> RemoveByImageModelId(string imageModelId)
        {
            var productImages = await _context.ProductImages.Where(productImage => productImage.ImageModelId == imageModelId).ToListAsync();
            if (productImages != null && productImages.Count > 0)
            {
                foreach (var productImage in productImages)
                {
                    _context.Remove(productImage);
                }
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> RemoveByProductId(string productId)
        {
            var productImages = await _context.ProductImages.Where(productImage => productImage.ProductId == productId).ToListAsync();
            if (productImages != null && productImages.Count > 0)
            {
                foreach (var productImage in productImages)
                {
                    _context.Remove(productImage);
                }
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
