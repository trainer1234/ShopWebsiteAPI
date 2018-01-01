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
    public class ProductOrderRepository : IProductOrderRepository
    {
        private ShopWebsiteSqlContext _context;

        public ProductOrderRepository(ShopWebsiteSqlContext context)
        {
            _context = context;
        }

        public void Add(ProductOrder newProductOrder)
        {
            _context.Add(newProductOrder);
            _context.SaveChanges();
        }

        public async Task<bool> Edit(ProductOrder newProductOrder)
        {
            var orderExist = await _context.ProductOrders.Where(order => order.OrderId == newProductOrder.OrderId)
                                        .Include(order => order.ProductMapOrderDetails)
                                            .ThenInclude(orderDetail => orderDetail.Product)
                                        .Include(order => order.Customer)
                                        .ToListAsync();
            if(orderExist != null && orderExist.Count > 0)
            {
                var searchOrder = orderExist[0];

                searchOrder.OrderStatus = newProductOrder.OrderStatus;
                searchOrder.TotalCost = newProductOrder.TotalCost;
                searchOrder.ProductAmount = newProductOrder.ProductAmount;

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
                                    .Include(order => order.Customer).ToListAsync();
            if(orders != null && orders.Count > 0)
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
                                                     .Include(order => order.Customer).ToListAsync();

            return orders;
        }

        public async Task<bool> Remove(string productOrderId)
        {
            var orders = await _context.ProductOrders.Where(order => order.OrderId == productOrderId)
                                    .Include(order => order.ProductMapOrderDetails)
                                         .ThenInclude(orderDetail => orderDetail.Product)
                                    .Include(order => order.Customer).ToListAsync();
            if (orders != null && orders.Count > 0)
            {
                _context.Remove(orders);
                _context.SaveChanges();

                return true;
            }
            return false;
        }
    }
}
