using Microsoft.EntityFrameworkCore;
using ShopWebsite.Common.Models.Enums;
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
    public class ProductOrderRepository : IProductOrderRepository
    {
        private ShopWebsiteSqlContext _context;

        public ProductOrderRepository(ShopWebsiteSqlContext context)
        {
            _context = context;
        }

        public string Add(ProductOrder newProductOrder)
        {
            _context.Add(newProductOrder);
            _context.SaveChanges();

            return newProductOrder.OrderId;
        }

        public async Task<bool> Edit(ProductOrder newProductOrder)
        {
            var orderExist = await _context.ProductOrders.Where(order => order.OrderId == newProductOrder.OrderId).ToListAsync();
            if(orderExist != null && orderExist.Count > 0)
            {
                var searchOrder = orderExist[0];

                searchOrder.OrderStatus = newProductOrder.OrderStatus;
                if(newProductOrder.TotalCost > 0)
                {
                    searchOrder.TotalCost = newProductOrder.TotalCost;
                }
                if(newProductOrder.ProductTotalAmount > 0)
                {
                    searchOrder.ProductTotalAmount = newProductOrder.ProductTotalAmount;
                }

                _context.Update(searchOrder);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<ProductOrder> Get(string orderId)
        {
            var orders = await _context.ProductOrders.Where(order => order.OrderId == orderId)
                                    .Include(order => order.ProductMapOrderDetails)
                                        .ThenInclude(orderDetail => orderDetail.Product)
                                            .ThenInclude(product => product.Manufacture)
                                    .Include(order => order.ProductMapOrderDetails)
                                        .ThenInclude(orderDetail => orderDetail.Product)
                                            .ThenInclude(product => product.ProductImages)
                                    .Include(order => order.ProductMapOrderDetails)
                                        .ThenInclude(orderDetail => orderDetail.Product)
                                            .ThenInclude(product => product.ProductProperties)
                                                .ThenInclude(productProp => productProp.Property)
                                    .Include(order => order.Customer).ToListAsync();
            if (orders != null && orders.Count > 0)
            {
                var order = orders[0];

                return order;
            }
            return null;
        }

        public async Task<List<ProductOrder>> GetAll()
        {
            var orders = await _context.ProductOrders.Include(order => order.ProductMapOrderDetails)
                                                        .ThenInclude(orderDetail => orderDetail.Product)
                                                            .ThenInclude(product => product.Manufacture)
                                                     .Include(order => order.ProductMapOrderDetails)
                                                        .ThenInclude(orderDetail => orderDetail.Product)
                                                            .ThenInclude(product => product.ProductImages)
                                                     .Include(order => order.ProductMapOrderDetails)
                                                        .ThenInclude(orderDetail => orderDetail.Product)
                                                            .ThenInclude(product => product.ProductProperties)
                                                                .ThenInclude(productProp => productProp.Property)
                                                     .Include(order => order.Customer).ToListAsync();

            return orders;
        }

        public async Task<List<ProductOrder>> FilterBy(OrderStatus orderStatus)
        {
            var orders = await _context.ProductOrders.Where(order => order.OrderStatus == orderStatus)
                                                     .Include(order => order.ProductMapOrderDetails)
                                                        .ThenInclude(orderDetail => orderDetail.Product)
                                                            .ThenInclude(product => product.Manufacture)
                                                     .Include(order => order.ProductMapOrderDetails)
                                                        .ThenInclude(orderDetail => orderDetail.Product)
                                                            .ThenInclude(product => product.ProductImages)
                                                     .Include(order => order.ProductMapOrderDetails)
                                                        .ThenInclude(orderDetail => orderDetail.Product)
                                                            .ThenInclude(product => product.ProductProperties)
                                                                .ThenInclude(productProp => productProp.Property)
                                                     .Include(order => order.Customer).ToListAsync();

            return orders;
        }

        public async Task<bool> Remove(string productOrderId)
        {
            var orders = await _context.ProductOrders.Where(order => order.OrderId == productOrderId).ToListAsync();
            if (orders != null && orders.Count > 0)
            {
                var order = orders[0];

                _context.Remove(order);
                _context.SaveChanges();

                return true;
            }
            return false;
        }
    }
}
