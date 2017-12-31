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
    public class ProductOrderDetailRepository : IProductOrderDetailRepository
    {
        private ShopWebsiteSqlContext _context;

        public ProductOrderDetailRepository(ShopWebsiteSqlContext context)
        {
            _context = context;
        }

        public void Add(ProductOrderDetail newProductOrderDetail)
        {
            _context.Add(newProductOrderDetail);
            _context.SaveChanges();
        }

        public async Task<ProductOrderDetail> Get(string productOrderId)
        {
            var productOrderDetail = await _context.ProductOrderDetails.Where(orderDetail => orderDetail.ProductOrderId == productOrderId)
                                                .Include(orderDetail => orderDetail.ProductMapOrderDetails)
                                                    .ThenInclude(orderMap => orderMap.Product).ToListAsync();
            if(productOrderDetail != null && productOrderDetail.Count > 0)
            {
                var searchOrderDetail = productOrderDetail[0];

                return searchOrderDetail;
            }
            return null;
        }

        public async Task<List<ProductOrderDetail>> GetAll()
        {
            var productOrderDetails = await _context.ProductOrderDetails.Include(orderDetail => orderDetail.ProductMapOrderDetails)
                                                                            .ThenInclude(orderMap => orderMap.Product).ToListAsync();

            return productOrderDetails;
        }

        public async Task<bool> Remove(string productOrderId)
        {
            var productOrderDetail = await _context.ProductOrderDetails.Where(orderDetail => orderDetail.ProductOrderId == productOrderId)
                                                .Include(orderDetail => orderDetail.ProductMapOrderDetails)
                                                    .ThenInclude(orderMap => orderMap.Product).ToListAsync();
            if (productOrderDetail != null && productOrderDetail.Count > 0)
            {
                var searchOrderDetail = productOrderDetail[0];

                _context.Remove(searchOrderDetail);
                _context.SaveChanges();

                return true;
            }
            return false;
        }
    }
}
