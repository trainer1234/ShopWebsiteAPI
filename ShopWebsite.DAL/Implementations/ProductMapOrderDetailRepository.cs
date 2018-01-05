using Microsoft.EntityFrameworkCore;
using ShopWebsite.DAL.Context;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.ProductOrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Implementations
{
    public class ProductMapOrderDetailRepository : IProductMapOrderDetailRepository
    {
        private ShopWebsiteSqlContext _context;

        public ProductMapOrderDetailRepository(ShopWebsiteSqlContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(ProductMapOrderDetail newProductMapOrderDetail)
        {
            var productMapOrderDetailExist = await _context.ProductMapOrderDetails
                .Where(productMap => productMap.ProductId == newProductMapOrderDetail.ProductId
                                        && productMap.ProductOrderId == newProductMapOrderDetail.ProductOrderId).ToListAsync();
            if(productMapOrderDetailExist != null && productMapOrderDetailExist.Count > 0)
            {
                return false;
            }
            else
            {
                _context.Add(newProductMapOrderDetail);
                _context.SaveChanges();

                return true;
            }
        }

        public async Task<bool> Remove(string productOrderDetailId, string productId)
        {
            var productMapOrderDetailExist = await _context.ProductMapOrderDetails
                                                   .Where(productMap => productMap.ProductId == productId
                                                        && productMap.ProductOrderId == productOrderDetailId).ToListAsync();
            if (productMapOrderDetailExist != null && productMapOrderDetailExist.Count > 0)
            {
                var search = productMapOrderDetailExist[0];

                _context.Remove(search);
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
            var productMapOrderDetailExist = await _context.ProductMapOrderDetails
                                                   .Where(productMap => productMap.ProductId == productId).ToListAsync();
            if (productMapOrderDetailExist != null && productMapOrderDetailExist.Count > 0)
            {
                foreach (var productMap in productMapOrderDetailExist)
                {
                    _context.Remove(productMap);
                }
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> RemoveByProductOrderDetailId(string productOrderDetailId)
        {
            var productMapOrderDetailExist = await _context.ProductMapOrderDetails
                                                   .Where(productMap => productMap.ProductId == productOrderDetailId).ToListAsync();
            if (productMapOrderDetailExist != null && productMapOrderDetailExist.Count > 0)
            {
                foreach (var productMap in productMapOrderDetailExist)
                {
                    _context.Remove(productMap);
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
